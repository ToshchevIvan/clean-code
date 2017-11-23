namespace Markdown.Renderers
{
    public class HTMLPairedTag : PairedTag
    {
        public HTMLPairedTag(string tagName, ITokenRenderer renderer) :
            base($"<{tagName}>", $"</{tagName}>", renderer)
        {
        }
    }
}
