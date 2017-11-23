using System.Collections.Generic;
using System.Linq;
using System.Text;
using Markdown.Renderers;


namespace Markdown.Tokens
{
    public class CompoundToken : IToken
    {
        public IReadOnlyList<IToken> InnerTokens { get; }

        public CompoundToken(IReadOnlyList<IToken> tokens) 
        {
            InnerTokens = tokens;
        }

        protected string[] RenderInnerTokens(ITokenRenderer renderer)
        {
            return InnerTokens.Select(t => t.Render(renderer))
                .ToArray();
        }

        public virtual string Render(ITokenRenderer renderer)
        {
            return string.Join("", RenderInnerTokens(renderer));
        }
    }
}
