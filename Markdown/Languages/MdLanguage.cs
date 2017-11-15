using Markdown.Tokens;


namespace Markdown.Languages
{
    internal static class MdLanguage
    {
        public static readonly TryRead[] Readers =
        {
            StrongTextToken.TryRead,
            EmphasizedTextToken.TryRead,
            PlanTextToken.TryRead
        };
    }
}
