namespace Markdown.Renderers
{
    public class PairedTag : ITag
    {
        public string OpeningTag { get; }
        public string ClosingTag { get; }

        private readonly ITokenRenderer renderer;

        internal PairedTag(string openingTag, string closingTag, ITokenRenderer renderer)
        {
            this.renderer = renderer;
            OpeningTag = openingTag;
            ClosingTag = closingTag;
        }

        public void Dispose() =>
            renderer.PushText(ClosingTag);
    }
}
