using System;
using System.Collections.Generic;
using System.Linq;
using Markdown.Tokens;


namespace Markdown
{
    public class TokenReader : IDisposable
    {
        public bool AtEnd => position == end;
        public bool ReadSuccessfully { private get; set; }
        
        private readonly string text;
        private int previousPosition;
        private int position;
        private int end;

        public TokenReader(string text, int offset, int end)
        {
            this.text = text;
            position = previousPosition = offset;
            this.end = end;
        }

        public TokenReader(string text, int offset) : this(text, offset, text.Length)
        {
        }

        public TokenReader(string text) : this(text, 0, text.Length)
        {
        }

        public IToken[] ReadTokens(IReadOnlyList<TryRead> readers)
        {
            var result = new List<IToken>();
            while (!AtEnd)
            {
                IToken token = null;
                var readResult = readers
                    .Any(r => r(this, out token));
                if (!readResult)
                    throw new ArgumentException();
                result.Add(token);
            }

            return result.ToArray();
        }

        public char CurrentChar => text[position];

        public string Read(int count)
        {
            var result = text.Substring(position, count);
            position += count;
            return result;
        }
        
        public string ReadWhile(Func<char, bool> accept)
        {
            var startPosition = position;
            for (; position < end; position++)
                if (!accept(text[position]))
                    break;
            return text.Substring(startPosition, position - startPosition);
        }
        
        public string ReadWhile(params char[] acceptableChars)
        {
            var acceptableCharsSet = new HashSet<char>(acceptableChars);
            return ReadWhile(acceptableCharsSet.Contains);
        }
        
        public string ReadUntil(Func<char, bool> isStopChar)
        {
            return ReadWhile(c => !isStopChar(c));
        }
        
        public string ReadUntil(params char[] stopChars)
        {
            var stopCharsSet = new HashSet<char>(stopChars);
            return ReadUntil(stopCharsSet.Contains);
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
