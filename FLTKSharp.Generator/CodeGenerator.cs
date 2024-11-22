namespace FLTKSharp.Generator
{
    public class CodeGenerator
    {
        private readonly RootCodeGenElement _root;
        public CodeGenerator(RootCodeGenElement root)
        {
            _root = root;
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
