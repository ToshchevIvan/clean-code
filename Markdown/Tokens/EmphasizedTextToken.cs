using System;
using Markdown.Languages;
using Markdown.Renderers;


namespace Markdown.Tokens
{
    public class EmphasizedTextToken : PlainTextToken
    {
        public EmphasizedTextToken(string content) : base(content)
        {
        }

        public override string Render(ITokenRenderer renderer) =>
            renderer.Render(this);
        
        public static bool TryRead(TokenReader reader, out IToken token)
        {
            token = null;
            if (reader.CurrentChar != MdLanguage.Delimiter)
                return false;

            using (reader)
            {
                reader.Read(1);
                var text = reader.ReadWhile(c => c != MdLanguage.Delimiter);
                if (text.StartsWith(" ") || reader.AtEnd ||
                    reader.Read(1) != MdLanguage.Delimiter.ToString() ||
                    char.IsWhiteSpace(text[text.Length - 1]))
                    return false;

                token = new EmphasizedTextToken(text);
                reader.ReadSuccessfully = true;
                return true;
            }
        }
    }
}
