using Markdown.Renderers;


namespace Markdown.Tokens
{
    public interface IToken
    {
        string Render(ITokenRenderer renderer);
    }
}
