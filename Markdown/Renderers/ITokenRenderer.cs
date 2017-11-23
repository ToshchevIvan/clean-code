using Markdown.Tokens;


namespace Markdown.Renderers
{
    public interface ITokenRenderer
    {
        ITag Emphasized();
        ITag Strong();
        ITag WithTag(ITag tag);
        void PushText(string text);
        string Render(IToken token);
    }
}
