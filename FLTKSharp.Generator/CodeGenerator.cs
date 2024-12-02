using NLog;

namespace FLTKSharp.Generator
{
    public class CodeGenerator
    {
        private readonly RootCodeGenElement _root;
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        public CodeGenerator(RootCodeGenElement root)
        {
            _root = root;
        }

        public void Execute()
        {
            var slnDirectory = Program.FindSolutionDirectory();
            foreach (var item in _root.Classes)
            {
                var content = GenerateClass(item);
                if (string.IsNullOrEmpty(content))
                {
                    _log.Error($"No content generated for {item.Name}");
                    continue;
                }
                var fn = Path.Join(slnDirectory, "FLTKSharp.Core", $"{item.Name}.Generated.cs");
                if (!File.Exists(fn))
                    File.Delete(fn);
                File.WriteAllText(fn, content);
            }
            _log.Info("Done");
        }

        public string? GenerateClass(CodeGenClassElement @class)
        {
            var containerContent = Program.GetEmbeddedResourceAsString($"CodeTemplate.{@class.Name}-Container.txt");
            if (string.IsNullOrEmpty(containerContent))
            {
                _log.Error($"Could not find template container for class {@class.Name}");
                return null;
            }

            var extends = BuildClassImplementations(@class);
            var body = new List<string>();
            foreach (var template in @class.Templates)
            {
                var res = Program.GetEmbeddedResourceAsString(template.ResourceName);
                if (res == null)
                {
                    _log.Error($"Could not find template resource {template.ResourceName} for class {@class.Name}");
                    continue;
                }
                
                body.Add("#region " + template.ResourceName);
                var x = res.Split("\n").Select(e => e.Split("\r")[0]);
                body.AddRange(x);
                body.Add("#endregion");
            }

            var bodyText = string.Join("\n", body.Select(e => "    " + e));

            var @using = "";

            var resultLines = containerContent
                .Replace("<!-- codegen:extends -->", extends)
                .Replace("<!-- codegen:body -->", bodyText)
                .Replace("<!-- codegen:using -->", @using)
                .Replace("%prefix%", @class.Prefix)
                .Replace("%name%", @class.Name)
                .Split("\n").Select(e => e.Split("\r")[0]).ToList();

            var fulfilled = FulfillTemplateTags(resultLines);
            _log.Info($"Created {fulfilled.Count} lines");
            return string.Join("\n", fulfilled);
        }
        
        

        private string BuildClassImplementations(CodeGenClassElement element)
        {
            var x = new List<string>();
            foreach (var item in element.Extends)
            {
                if (!string.IsNullOrEmpty(item.Name))
                {
                    x.Add(item.Name);
                }
            }

            var s = string.Join(", ", x);
            if (!string.IsNullOrEmpty(s))
            {
                return ": " + s;
            }

            return "";
        }

        private List<string> FulfillTemplateTags(List<string> incoming)
        {
            return FulfillTemplateTags(incoming, out var _);
        }
        private List<string> FulfillTemplateTags(List<string> incoming, out int count)
        {
            var result = new List<string>();
            count = 0;
            foreach (var line in incoming)
            {
                var parsed = ParseTagData(line);
                if (parsed == null || string.IsNullOrEmpty(parsed.Value.Item2))
                {
                    result.Add(line);
                    continue;
                }
                if (parsed.Value.Item1 != "template")
                {
                    result.Add(line);
                    continue;
                }
                bool f = false;
                foreach (var p in _root.Templates)
                {
                    if (p.Name == parsed.Value.Item2)
                    {
                        result.Add(line.Replace($"--{parsed.Value.Item1}:{parsed.Value.Item2}", p.Text.Trim()));
                        count++;
                        f = true;
                        break;
                    }
                }
                if (f == false)
                {
                    result.Add(line);
                }
            }
            return result;
        }

        private static (string, string?)? ParseTagData(string line)
        {
            if (!line.Trim().StartsWith("--"))
                return null;
            var dashIndex = line.IndexOf("--");
            var start = line.Substring(dashIndex + 2);
            var sepIndex = start.IndexOf(":");
            if (sepIndex == -1)
                return (start, null);
            var key = start.Substring(0, sepIndex);
            var val = start.Substring(sepIndex + 1, start.Length - sepIndex - 1);
            if (string.IsNullOrEmpty(val))
                return (key, null);
            return (key, val);
        }
    }
}
