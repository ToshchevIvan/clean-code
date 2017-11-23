using System.Collections.Generic;
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

        public virtual string Render(ITokenRenderer renderer)
        {
            var builder = new StringBuilder(InnerTokens.Count);
            foreach (var innerToken in InnerTokens)
                builder.Append(innerToken.Render(renderer));

            return builder.ToString();
        }
    }
}
