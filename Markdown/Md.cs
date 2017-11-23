using System;
using Markdown.Languages;
using Markdown.Renderers;
using Markdown.Tokens;


namespace Markdown
{
    public class Md
    {
        public string RenderToHtml(string markdown)
        {
            if (markdown == null)
                throw new ArgumentNullException(nameof(markdown));
            var reader = new TokenReader(new MdLanguage(), markdown);
            var token = new CompoundToken(reader.ReadTokens());
            var renderer = new HTMLTokenRenderer();

            return token.Render(renderer);
        }
    }
}
