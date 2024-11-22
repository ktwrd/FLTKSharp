namespace FLTKSharp.Generator
{
    public class CodeGenerator
    {
        private readonly RootCodeGenElement _root;
        public CodeGenerator(RootCodeGenElement root)
        {
            _root = root;
        }

        private List<string> FulfillTemplateTags(List<string> incoming, out int count)
        {
            var result = new List<string>();
            count = 0;
            foreach (var line in incoming)
            {
                var parsed = ParseTagData(line);
                if (parsed == null || (parsed.HasValue && string.IsNullOrEmpty(parsed.Value.Item2)))
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
                        result.Add(p.Text);
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
            if (!line.StartsWith("--"))
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
