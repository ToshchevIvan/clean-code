using System.Collections.Generic;
using System.Text;


namespace Markdown.Renderers
{
    // ReSharper disable once InconsistentNaming
    internal class HTMLTokenRenderer : ITokenRenderer
    {
        public string Emphasized(params string[] text) =>
            PairedTag("em", text);

        public string Strong(params string[] text) =>
            PairedTag("strong", text);

        private static string PairedTag(string tagName, IEnumerable<string> text)
        {
            var builder = new StringBuilder();
            builder.Append($"<{tagName}>");
            foreach (var s in text)
                builder.Append(s);
            builder.Append($"</{tagName}>");
            return builder.ToString();
        }
    }
}
