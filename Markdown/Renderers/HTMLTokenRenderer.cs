using System.Text;
using Markdown.Tokens;


namespace Markdown.Renderers
{
    // ReSharper disable once InconsistentNaming
    internal class HTMLTokenRenderer : ITokenRenderer
    {
        public string Render(PlainTextToken token) =>
            token.Content;

        public string Render(EmphasizedTextToken token) =>
            $"<em>{token.Content}</em>";
        
        public string Render(StrongTextToken token) =>
            $"<strong>{Render((CompoundToken) token)}</strong>";

        public string Render(CompoundToken token)
        {
            var builder = new StringBuilder(token.InnerTokens.Count);
            foreach (var innerToken in token.InnerTokens)
                builder.Append(innerToken.Render(this));

            return builder.ToString();
        }
    }
}
