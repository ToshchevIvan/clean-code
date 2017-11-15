using Markdown.Renderers;


namespace Markdown.Tokens
{
    public interface IToken
    {
        string Content { get; }
        string Render(ITokenRenderer renderer);
    }
}
