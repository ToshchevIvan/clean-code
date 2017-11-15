using Markdown.Tokens;


namespace Markdown.Renderers
{
    internal class HTMLTokenRenderer : ITokenRenderer
    {
        public string Render(PlanTextToken token) =>
            token.Content;

        public string Render(EmphasizedTextToken token) =>
            $"<em>{token.Content}</em>";
        
        public string Render(StrongTextToken token) =>
            $"<strong>{token.Content}</strong>";
    }
}
