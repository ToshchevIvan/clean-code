using System;
using System.Collections.Generic;


namespace Markdown
{
    public class TokenReader : IDisposable
    {
        public int Position { get; private set; }
        public bool AtEnd => Position == text.Length - 1;
        public bool ReadSuccesfully { private get; set; }

        public TokenReader(string text)
        {
            this.text = text;
        }

        public string ReadUntil(params char[] stopChars)
        {
            var stopCharsSet = new HashSet<char>(stopChars);
            return ReadUntil(stopCharsSet.Contains);
        }

        public string ReadUntil(Func<char, bool> isStopChar)
        {
            var startPosition = truePosition;
            for (; truePosition < text.Length; truePosition++)
                if (isStopChar(text[truePosition]))
                    break;
            return text.Substring(startPosition, truePosition - startPosition);
        }

        public string ReadWhile(params char[] acceptableChars)
        {
            throw new NotImplementedException();
        }

        public string ReadWhile(Func<char, bool> accept)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (ReadSuccesfully)
            {
                ReadSuccesfully = false;
                Position = truePosition;
            }
            else
            {
                truePosition = Position;
            }
        }

        private readonly string text;
        private int truePosition;
    }
}
