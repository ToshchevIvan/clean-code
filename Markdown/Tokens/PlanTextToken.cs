using Markdown.Renderers;


namespace Markdown.Tokens
{
    public class PlanTextToken : Token
    {
        public PlanTextToken(string content) : base(content)
        {
        }

        public override string Render(ITokenRenderer renderer) =>
            renderer.Render(this);
        
        public static bool TryRead(TokenReader reader, out IToken token)
        {
            using (reader)
            {
                var str = reader.ReadWhile(char.IsLetter);
                token = null;
                if (string.IsNullOrEmpty(str)) 
                    return false;
                token = new PlanTextToken(str);
                reader.ReadSuccesfully = true;
                return true;
            }
        }
    }
}
