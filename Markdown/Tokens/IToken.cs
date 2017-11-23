using Markdown.Renderers;


namespace Markdown.Tokens
{
    public interface IToken
    {
        void Render(ITokenRenderer renderer);
    }
}
