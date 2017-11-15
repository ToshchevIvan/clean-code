using System;
using System.Collections.Generic;
using System.Linq;
using Markdown.Languages;
using Markdown.Tokens;


namespace Markdown
{
    public class Md
    {
        public string RenderToHtml(string markdown)
        {
            if (markdown == null)
                throw new ArgumentNullException(nameof(markdown));
            var reader = new TokenReader(markdown);
            var tokens = new List<IToken>();
            while (!reader.AtEnd)
            {
                IToken token = null;
                var readResult = MdLanguage.Readers
                    .Any(r => r(reader, out token));
                if (!readResult)
                    throw new ArgumentException();
                tokens.Add(token);
            }
            return markdown;
        }
    }
}
