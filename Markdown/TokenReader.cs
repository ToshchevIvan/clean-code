using System;
using System.Collections.Generic;
using System.Linq;
using Markdown.Languages;
using Markdown.Tokens;


namespace Markdown
{
    public class TokenReader : IDisposable
    {
        public bool AtEnd => position == end;
        public bool ReadSuccessfully { private get; set; }
        public int LengthLeft => end - position;

        private readonly ILanguage language;
        private readonly string text;
        private int previousPosition;
        private int position;
        private int end;

        public TokenReader(ILanguage language, string text, int offset, int end)
        {
            this.language = language;
            this.text = text;
            position = previousPosition = offset;
            this.end = end;
        }

        public TokenReader(ILanguage readers, string text, int offset) : this(readers, text, offset, text.Length)
        {
        }

        public TokenReader(ILanguage language, string text) : this(language, text, 0, text.Length)
        {
        }

        public TokenReader(TokenReader parent, int padding) 
            : this(parent.language, parent.text, parent.previousPosition + padding, parent.position - padding)
        {
        }

        public void RollBack(int count)
        {
            var shift = position - count;
            position = shift < 0 ? 0 : shift;
        }

        public IEnumerable<char> ReadChars()
        {
            for (; position < end; position++)
                yield return text[position];
        }

        public IToken[] ReadTokens()
        {
            var result = new List<IToken>();
            while (!AtEnd)
            {
                IToken token = null;
                var readResult = language.TokenReaders
                    .Any(r => r(this, out token));
                if (!readResult)
                    throw new ArgumentException();
                result.Add(token);
            }

            return result.ToArray();
        }

        public bool StartsWith(string value)
        {
            if (value.Length > LengthLeft)
                return false;
            return text.Substring(position, value.Length) == value;
        }

        public string Read(int count)
        {
            var result = text.Substring(position, count);
            position += count;
            return result;
        }
        
        public void Dispose()
        {
            if (ReadSuccessfully)
            {
                previousPosition = position;
                ReadSuccessfully = false;
            }
            else
            {
                position = previousPosition;
            }
        }
    }
}
