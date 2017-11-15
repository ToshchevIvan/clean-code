using System;
using Markdown.Renderers;


namespace Markdown.Tokens
{
    public class EmphasizedTextToken : Token
    {
        public EmphasizedTextToken(string content) : base(content)
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
