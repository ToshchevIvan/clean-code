using System.Collections.Generic;
using Markdown.Tokens;


namespace Markdown.Languages
{
    public interface ILanguage
    {
        IEnumerable<TryRead> TokenReaders { get; }
    }
}
