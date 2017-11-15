using Markdown.Tokens;


namespace Markdown.Renderers
{
    public interface ITokenRenderer
    {
        string Render(PlanTextToken token);
        string Render(EmphasizedTextToken token);
        string Render(StrongTextToken token);
    }
}
