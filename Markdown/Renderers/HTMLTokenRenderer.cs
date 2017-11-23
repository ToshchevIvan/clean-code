using System.Text;
using Markdown.Tokens;


namespace Markdown.Renderers
{
    // ReSharper disable once InconsistentNaming
    internal class HtmlTokenRenderer : ITokenRenderer
    {
        private readonly PairedTag emphasizedTag;
        private readonly PairedTag strongTag;
        private StringBuilder builder;

        public HtmlTokenRenderer()
        {
            builder = new StringBuilder();
            emphasizedTag = new HTMLPairedTag("em", this);
            strongTag = new HTMLPairedTag("strong", this);
        }

        public ITag Emphasized() =>
            WithTag(emphasizedTag);

        public ITag Strong() =>
            WithTag(strongTag);

        public void PushText(string text) => builder.Append(text);

        public string Render(IToken token)
        {
            builder = new StringBuilder();
            token.Render(this);
            return builder.ToString();
        }

        public ITag WithTag(ITag tag)
        {
            builder.Append(tag.OpeningTag);
            return tag;
        }
    }
}
