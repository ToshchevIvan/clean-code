using Markdown.Tokens;


namespace Markdown.Renderers
{
    public interface ITokenRenderer
    {
        string Render(PlainTextToken token);
        string Render(EmphasizedTextToken token);
        string Render(StrongTextToken token);
        string Render(CompoundToken token);
    }
}
