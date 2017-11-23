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

        public virtual void Render(ITokenRenderer renderer)
        {
            foreach (var innerToken in InnerTokens)
                innerToken.Render(renderer);
        }
    }
}
