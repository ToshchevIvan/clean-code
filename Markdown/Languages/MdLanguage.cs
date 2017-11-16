using Markdown.Tokens;


namespace Markdown.Languages
{
    internal static class MdLanguage
    {
        public const char Delimiter = '_';

        public static readonly TryRead[] Readers =
        {
//            StrongTextToken.TryRead,
            EmphasizedTextToken.TryRead,
            PlainTextToken.TryRead
        };
    }
}
