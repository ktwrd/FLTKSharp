using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;

namespace FLTKSharp.Generator;

public static class Program
{
    public static void Main(string[] args)
    {
        // Generate Bindings
        var solutionDirectory = FindSolutionDirectory();
        Console.WriteLine(solutionDirectory);
        var bindingData = ParseData<RootBindingElement>("bindings.xml");
        var gen = new BindingGenerator(bindingData);
        gen.GenerateAndWrite(Path.Join(solutionDirectory, "FLTKSharp.Core", "CFltkNative.Generated.cs"));

        var codeData = ParseData<RootCodeGenElement>("code.xml");
        var code = new CodeGenerator(codeData);
        code.Execute();
    }

    public static string FindSolutionDirectory()
    {
        if (File.Exists("FLTKSharp.sln"))
        {
            return Directory.GetCurrentDirectory();
        }
        else if (File.Exists("FLTKSharp.Generator.csproj"))
        {
            return Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;
        }
        else
        {
            string target = Directory.GetCurrentDirectory()!;
            bool state = true;
            while (state)
            {
                var d = Directory.GetParent(target);
                if (d == null)
                {
                    throw new InvalidOperationException($"Failed to get parent directory for {target}");
                }
                target = d.FullName;
                if (File.Exists(Path.Join(target, "FLTKSharp.sln")))
                {
                    state = false;
                }
            }
            return target;
        }
    }

    static Stream? GetEmbeddedResource(string resourceName)
    {
        return typeof(Program).Assembly.GetManifestResourceStream($"FLTKSharp.Generator.{resourceName}");
    }

    public static string? GetEmbeddedResourceAsString(string resourceName)
    {
        var stream = GetEmbeddedResource(resourceName);
        if (stream == null)
            return null;
        using var sr = new StreamReader(stream);
        return sr.ReadToEnd();
    }

    public static T ParseData<T>(string resourceName) where T : class, new()
    {
        using var inputStream = GetEmbeddedResource(resourceName);
        if (inputStream == null)
        {
            throw new InvalidDataException($"Stream from {nameof(GetEmbeddedResource)}(\"{resourceName}\") returned null");
        }

        using (XmlReader reader = XmlReader.Create(inputStream, new (){ IgnoreWhitespace = true }))
        {
            var ser = new XmlSerializer(typeof(T));
            return (ser.Deserialize(reader) as T) ?? new();
        }
    }
}
