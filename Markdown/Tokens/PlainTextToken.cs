using System;
using Markdown.Languages;
using Markdown.Renderers;


namespace Markdown.Tokens
{
    public class PlainTextToken : IToken
    {
        public string Content { get; }
        
        public PlainTextToken(string content)
        {
            Content = content;
        }

        public virtual string Render(ITokenRenderer renderer) =>
            renderer.Render(this);
        
        public static bool TryRead(TokenReader reader, out IToken token)
        {
            using (reader)
            {
                var precedingDelims = reader.ReadWhile(MdLanguage.Delimiter);
                var text = precedingDelims + reader.ReadUntil(MdLanguage.Delimiter);
                if (text.Length > 0)
                {
                    token = new PlainTextToken(text);
                    reader.ReadSuccessfully = true;
                    return true;
                }
                token = null;
                return false;
            }
        }
    }
}
