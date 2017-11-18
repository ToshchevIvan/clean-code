using System.Collections.Generic;
using Markdown.Tokens;


namespace Markdown.Languages
{
    internal class MdLanguage : ILanguage
    {
        public IEnumerable<TryRead> TokenReaders => tokenReaders;

        private static readonly TryRead[] tokenReaders =
        {
            StrongTextToken.TryRead,
            EmphasizedTextToken.TryRead,
            PlainTextToken.TryRead
        };
    }
}
