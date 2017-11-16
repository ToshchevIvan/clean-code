using System;
using System.Collections.Generic;
using Markdown.Renderers;


namespace Markdown.Tokens
{
    public class StrongTextToken : CompoundToken
    {
        public StrongTextToken(IReadOnlyList<IToken> tokens) : base(tokens)
        {
        }
        
        public override string Render(ITokenRenderer renderer) =>
            renderer.Render(this);

        public static bool TryRead(TokenReader reader, out IToken token)
        {
            throw new NotImplementedException();
        }
    }
}
