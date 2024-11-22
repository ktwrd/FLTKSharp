namespace FLTKSharp.Generator
{
    /// <summary>
    /// Used for generating the C bindings, which use <see cref="DllImportAttribute"/>
    /// </summary>
    public class BindingGenerator
    {
        private readonly RootBindingElement _root;
        public BindingGenerator(RootBindingElement root)
        {
            _root = root;
        }
        public List<string> GenerateContent()
        {
            var generatedLines = new List<string>();

            foreach (var group in _root.Groups)
            {
                var d = ProcessGroup(group);
                generatedLines.Add($"#region {group.Name}");
                generatedLines.AddRange(d);
                generatedLines.AddRange([
                    "#endregion",
                    ""
                ]);
            }

            string indent = "".PadLeft(4, ' ');
            generatedLines = generatedLines.SelectMany(v => v.Split("\n"))
                .Select(v => (indent + v).TrimEnd())
                .ToList();
            generatedLines.InsertRange(0, [
                "using System.Runtime.InteropServices;",
                "",
                "namespace FLTKSharp.Core;",
                "",
                "// ReSharper disable All",
                "public class CFltkNative",
                "{"
            ]);
            generatedLines.Add("}");

            return generatedLines;
        }
        public void GenerateAndWrite(string targetFile)
        {
            var content = GenerateContent();
            if (File.Exists(targetFile))
                File.Delete(targetFile);
            File.WriteAllLines(targetFile, content);
            Console.WriteLine($"Wrote {content.Count} lines to {targetFile}");
        }

        private List<string> ProcessGroup(BindingGroupElement group)
        {
            var resultLines = new List<string>();
            var functions = FindFunctions(group).Select(v => v.Value).ToList();

            for (int i = 0; i < functions.Count; i++)
            {
                var d = ProcessFunction(group, functions[i]);
                resultLines.AddRange(d);
                if (i < (functions.Count - 1))
                    resultLines.Add("");
            }

            return resultLines;
        }
        private Dictionary<string, BindingFunctionElement> FindFunctions(BindingGroupElement group)
        {
            var result = new Dictionary<string, BindingFunctionElement>();
            foreach (var inheritFromGroup in group.InheritFrom ?? [])
            {
                foreach (var referencedGroup in _root.Groups)
                {
                    if (referencedGroup.Name == inheritFromGroup.Name)
                    {
                        foreach (var function in referencedGroup.Functions)
                        {
                            if (!function.InheritBlacklist)
                            {
                                result[function.Name] = function;
                            }
                        }
                    }
                }
            }
            foreach (var function in group.Functions)
            {
                result[function.Name] = function;
            }
            return result;
        }

        private List<string> ProcessFunction(BindingGroupElement group, BindingFunctionElement func)
        {
            var parameters = BuildFunctionParameters(group, func, out var parameterXmlComments);
            var xmlComments = new List<string>();
            if (!string.IsNullOrEmpty(func.Summary))
            {
                var s = func.Summary.Split("\n").Select(v => "/// " + v.Split("\r")[0]).Where(v => !string.IsNullOrEmpty(v)).ToList();
                if (s.Count > 0)
                {
                    xmlComments.AddRange(s);
                }
            }
            if (GenerateParameterCommentsRequired(parameterXmlComments))
            {
                foreach (var (name, value) in parameterXmlComments)
                {
                    var valueCorrect = value.Split("\n")
                        .Select(v => v.Split("\r")[0])
                        .Where(v => !string.IsNullOrEmpty(v))
                        .Select(v => $"/// {v}")
                        .ToList();
                    xmlComments.Add($"/// <param name=\"{name}\">");
                    xmlComments.AddRange(valueCorrect);
                    xmlComments.Add($"/// </param>");
                }
            }
            string returnType = "void";
            if (func.Returns != null && func.Returns.Type.Trim().ToLower() != "void")
            {
                returnType = func.Returns.Type;
                string comment = func.Returns.InheritFrom?.ToXmlString()?.Replace("$groupname", group.Name) ?? "";
                if (!string.IsNullOrEmpty(func.Returns.Summary))
                {
                    comment += "\n" + func.Returns.Summary;
                }

                if (!string.IsNullOrEmpty(comment))
                {
                    var commentCorrect = comment.Split("\n")
                        .Select(v => v.Split("\r")[0])
                        .Where(v => !string.IsNullOrEmpty(v))
                        .Select(v => $"/// {v}")
                        .ToList();
                    xmlComments.Add("/// <returns>");
                    xmlComments.AddRange(commentCorrect);
                    xmlComments.Add("/// </returns>");
                }
            }

            string functionName = $"{group.Name}_{func.Name}";

            string definition = string.Format("{0}({1})",
                functionName,
                string.Join(", ", parameters));

            var resultLines = new List<string>();
            resultLines.AddRange(xmlComments);
            resultLines.AddRange([
                $"[DllImport(Constants.LibraryFilename, EntryPoint = \"{functionName}\", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]",
                $"public static extern {returnType} {definition};"
            ]);
            return resultLines;
        }
        private static bool GenerateParameterCommentsRequired(Dictionary<string, string> comments)
        {
            foreach (var (k, v) in comments)
            {
                if (!string.IsNullOrEmpty(v.Trim()))
                    return true;
            }
            return false;
        }
        private List<string> BuildFunctionParameters(BindingGroupElement group, BindingFunctionElement func, out Dictionary<string, string> xmlComments)
        {
            var result = new List<string>();
            xmlComments = [];
            foreach (var parameter in func.Parameters)
            {
                result.Add($"{parameter.Type} {parameter.Name}");

                string comment = parameter.InheritFrom?.ToXmlString()?.Replace("$groupname", group.Name) ?? "";
                if (!string.IsNullOrEmpty(parameter.Summary))
                {
                    comment += "\n" + parameter.Summary;
                }
                xmlComments[parameter.Name] = comment;
            }
            return result;
        }
    }
}
