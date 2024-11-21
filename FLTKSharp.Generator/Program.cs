using System.ComponentModel;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace FLTKSharp.Generator;

class Program
{
    static void Main(string[] args)
    {
        var data = ParseData();

        var generatedLines = new List<string>();
        foreach (var group in data.Groups)
        {
            var items = new Dictionary<string, XmlGroupFunctionObject>();
            foreach (var inheritFromGroup in group.InheritFrom ?? [])
            {
                foreach (var referencedGroup in data.Groups)
                {
                    if (referencedGroup.Name == inheritFromGroup.Name)
                    {
                        foreach (var function in referencedGroup.Functions)
                        {
                            if (!function.InheritBlacklist)
                            {
                                items[function.Name] = function;
                            }
                        }
                    }
                }
            }
            foreach (var function in group.Functions)
            {
                items[function.Name] = function;
            }

            foreach (var (_, function) in items)
            {
                string functionName = $"{group.Name}_{function.Name}";
                var parameterList = new List<string>();
                var parameterXmlComments = new Dictionary<string, string>();
                foreach (var item in function.Parameters)
                {
                    parameterList.Add($"{item.Type} {item.Name}");
                    string commentContent = "";
                    if (item.InheritFrom != null)
                    {
                        string p = "";
                        if (!string.IsNullOrEmpty(item.InheritFrom.Path))
                        {
                            p = $"path=\"{item.InheritFrom.Path.Trim()}\" ";
                        }
                        var c = item.InheritFrom.CodeReference.Replace("$groupname", group.Name);

                        commentContent = $"<inheritdoc cref=\"{c}\" {p}/>";
                    }

                    if (!string.IsNullOrEmpty(item.Summary))
                    {
                        if (string.IsNullOrEmpty(commentContent))
                        {
                            commentContent = item.Summary.Replace("$groupname", group.Name);
                        }
                        else
                        {
                            commentContent += "\n" + item.Summary.Replace("$groupname", group.Name);
                        }
                    }

                    parameterXmlComments[item.Name] = commentContent;
                }
                string def = $"{functionName}({string.Join(", ", parameterList)})";
                string returnType = function.Returns == null ? "void" : function.Returns.Type;
                if (string.IsNullOrEmpty(returnType))
                    returnType = "void";
                if (!string.IsNullOrEmpty(function.Summary))
                {
                    var spr = function.Summary.Replace("$groupname", group.Name);
                    var spl = string.Join("\n", spr.Split("\n").Select(h => "/// " + h.Split("\r")[0]));
                    generatedLines.Add($"/// <summary>\n{spl}\n/// </summary>");
                }
                else
                {
                    generatedLines.Add($"/// <summary>Call the {functionName} function</summary>");
                }

                if (parameterXmlComments.Count > 0)
                {
                    foreach (var (k, v) in parameterXmlComments)
                    {
                        string kr = k.Replace("$groupname", group.Name);
                        if (string.IsNullOrEmpty(v))
                        {
                            generatedLines.Add($"/// <param name=\"{kr}\"></param>");
                        }
                        else
                        {
                            generatedLines.AddRange([
                                $"/// <param name=\"{kr}\">",
                                ..v.Split("\n").Select(m => "/// " + m.Split("\r")[0]),
                                "/// </param>"
                            ]);
                        }
                    }
                }

                if (function.Returns != null)
                {
                    string returnText = function.Returns.Summary ?? "";
                    if (function.Returns.InheritFrom != null)
                    {
                        string p = "";
                        if (!string.IsNullOrEmpty(function.Returns.InheritFrom.Path))
                        {
                            p = $"path=\"{function.Returns.InheritFrom.Path.Trim()}\" ";
                        }

                        if (!string.IsNullOrEmpty(returnText))
                        {
                            returnText += "\n";
                        }

                        var c = function.Returns.InheritFrom.CodeReference.Replace("$groupname", group.Name);
                        returnText += $"<inheritdoc cref=\"{c}\" {p}/>";
                    }

                    if (string.IsNullOrEmpty(returnText))
                    {
                        generatedLines.Add("/// <returns></returns>");
                    }
                    else
                    {
                        generatedLines.Add(string.Join("\n", new List<string>
                            {"/// <returns>"}.Concat(returnText.Split("\n").Select(u => "/// " + u.Split("\r")[0]).ToArray()).Concat(["/// </returns>"])
                        ));
                    }
                }
                
                generatedLines.AddRange([
                    $"[DllImport(Constants.LibraryFilename, EntryPoint = \"{functionName}\", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]",
                    $"public static extern {returnType} {def};",
                    ""
                ]);
            }
        }

        generatedLines = generatedLines.SelectMany(v => v.Split("\n")).Select(v => $"    {v}").ToList();
        generatedLines.InsertRange(0, [
            "using System.Runtime.InteropServices;",
            "",
            "namespace FLTKSharp.Core;",
            "",
            "// ReSharper disable All",
            "public class CFltkNative",
            "{",
        ]);
        generatedLines.Add("}");
        File.WriteAllText("CFltkNative.cs", string.Join(Environment.NewLine, generatedLines));
        if (Path.GetFileName(Directory.GetCurrentDirectory()) == "FLTKSharp.Generator")
        {
            if (File.Exists("../FLTKSharp.Core/CFltkNative.Generated.cs"))
                File.Delete("../FLTKSharp.Core/CFltkNative.Generated.cs");
            File.Copy("CFltkNative.cs", "../FLTKSharp.Core/CFltkNative.Generated.cs");
            File.Delete("CFltkNative.cs");
        }
        else if (File.Exists("FLTKSharp.sln"))
        {
            if (File.Exists("./FLTKSharp.Core/CFltkNative.Generated.cs"))
                File.Delete("./FLTKSharp.Core/CFltkNative.Generated.cs");
            File.Copy("CFltkNative.cs", "./FLTKSharp.Core/CFltkNative.Generated.cs");
            File.Delete("CFltkNative.cs");
        }
        else
        {
            string dp = Directory.GetCurrentDirectory();
            string d = Path.GetFileName(Directory.GetCurrentDirectory());
            while (d != "FLTKSharp.Generator")
            {
                var par = Directory.GetParent(dp);
                d = par.Name;
                dp = par.FullName;
            }

            var dest = Path.Join(Directory.GetParent(dp).FullName,
                "FLTKSharp.Core",
                "CFltkNative.Generated.cs");
            if (File.Exists(dest))
                File.Delete(dest);
            File.Copy("CFltkNative.cs", dest);
            File.Delete("CFltkNative.cs");
            Console.WriteLine($"Wrote file to {dest}");
        }
    }

    static string ToXML(XmlData data)
    {
        var ms = new MemoryStream();
        var wr = XmlWriter.Create(ms, new() {Indent = true, IndentChars = "    "});
        var des = new XmlSerializer(typeof(XmlData));
        des.Serialize(wr, data);
        return Encoding.UTF8.GetString(ms.ToArray());
    }

    static Stream? GetEmbeddedResource(string resourceName)
    {
        return typeof(Program).Assembly.GetManifestResourceStream($"FLTKSharp.Generator.{resourceName}");
    }

    static XmlData ParseData()
    {
        using var inputStream = GetEmbeddedResource("data.xml");
        if (inputStream == null)
        {
            throw new InvalidDataException($"Stream from {nameof(GetEmbeddedResource)}(\"data.xml\") returned null");
        }

        using (XmlReader reader = XmlReader.Create(inputStream, new (){ IgnoreWhitespace = true }))
        {
            var ser = new XmlSerializer(typeof(XmlData));
            return (ser.Deserialize(reader) as XmlData) ?? new();
        }
    }
}

[XmlRoot("Definition")]
public class XmlData
{
    [XmlElement("Group")]
    public List<XmlGroupObject> Groups { get; set; } = [];
}
public class XmlGroupObject
{
    [XmlAttribute("Name")]
    public string Name { get; set; } = "";
    
    [XmlElement("Function")]
    public List<XmlGroupFunctionObject> Functions { get; set; } = [];
    
    [XmlElement("InheritFrom", IsNullable = true)]
    public List<XmlGroupInheritFromObject>? InheritFrom { get; set; }
}

public class XmlGroupInheritFromObject
{
    [XmlAttribute("Name")] public string Name { get; set; } = "";
}

public class XmlGroupFunctionObject
{
    [XmlAttribute("Name")] public string Name { get; set; } = "";
    [XmlElement] public string? Summary { get; set; }
    
    [XmlElement("Parameter")]
    public List<XmlGroupFunctionParameterObject> Parameters { get; set; } = [];
    
    [XmlElement("Returns")]
    public XmlGroupFunctionReturnsObject? Returns { get; set; }

    [XmlAttribute("InheritBlacklist")]
    [DefaultValue(false)]
    public bool InheritBlacklist { get; set; } = false;
}

public class XmlGroupFunctionParameterObject
{
    [XmlAttribute("Name")] public string Name { get; set; } = "";
    [XmlAttribute("Type")] public string Type { get; set; } = "";
    [XmlAttribute("Summary")] public string? Summary { get; set; }
    [XmlElement("InheritFrom")] public XmlInheritFromObject? InheritFrom { get; set; }
}

public class XmlGroupFunctionReturnsObject
{
    [XmlAttribute("Type")] public string Type { get; set; } = "";

    [XmlAttribute("Summary")] public string? Summary { get; set; }
    
    [XmlElement("InheritFrom")] public XmlInheritFromObject? InheritFrom { get; set; }
}

public class XmlInheritFromObject
{
    [XmlAttribute("cref")]
    public string CodeReference { get; set; }
    [XmlAttribute("path")]
    public string Path { get; set; }
}