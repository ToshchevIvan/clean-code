using System.Collections.Generic;
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
        
        public virtual string Render(ITokenRenderer renderer) =>
            renderer.Render(this);
    }
}
