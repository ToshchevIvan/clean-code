using System;
using System.Collections.Generic;
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
            emphasizedTag = new PairedTag("em", this);
            strongTag = new PairedTag("strong", this);
        }

        public IDisposable Emphasized() => 
            WithTag(emphasizedTag);

        public IDisposable Strong() => 
            WithTag(strongTag);

        public void PushText(string text) => builder.Append(text);
        
        public string Render(IToken token)
        {
            builder = new StringBuilder();
            token.Render(this);
            return builder.ToString();
        }

        private IDisposable WithTag(PairedTag tag)
        {
            builder.Append(tag.OpeningTag);
            return tag;
        }
        
        private class PairedTag : IDisposable
        {
            internal readonly string OpeningTag;
            internal readonly string ClosingTag;
            
            private readonly ITokenRenderer renderer;

            internal PairedTag(string tagName, ITokenRenderer renderer)
            {
                this.renderer = renderer;
                OpeningTag = $"<{tagName}>";
                ClosingTag = $"</{tagName}>";
            }
            
            public void Dispose() =>
                renderer.PushText(ClosingTag);
        }
    }
}
