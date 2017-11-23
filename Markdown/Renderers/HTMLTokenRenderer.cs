namespace Markdown.Renderers
{
    // ReSharper disable once InconsistentNaming
    internal class HTMLTokenRenderer : ITokenRenderer
    {
        public string Emphasized(string text) =>
            $"<em>{text}</em>";

        public string Strong(string text) =>
            $"<strong>{text}</strong>";
    }
}
