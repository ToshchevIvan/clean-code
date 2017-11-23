namespace Markdown.Renderers
{
    public interface ITokenRenderer
    {
        string Emphasized(string text);
        string Strong(string text);
    }
}
