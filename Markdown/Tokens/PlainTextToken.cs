using System;
using System.Collections.Generic;
using Markdown.Languages;
using Markdown.Renderers;


namespace Markdown.Tokens
{
    public class PlainTextToken : IToken
    {
        public string Content { get; }
        private const char Delimiter = '_';

        private static readonly FsmReader FsmReader = new FsmReader(
            new Dictionary<State, Transition>
            {
                {
                    State.Start,
                    new Transition()
                        .Add(State.PlainText, Transition.NeutralTransducer, CharType.WhiteSpace, CharType.Alpha,
                            CharType.Numeric)
                        .Add(State.DelimitersAcc, Transition.NeutralTransducer, CharType.Delimiter)
                        .Add(State.Escaped, null, CharType.Escape)
                },
                {
                    State.Escaped,
                    new Transition()
                        .Add(State.PlainText, Transition.NeutralTransducer, CharType.WhiteSpace,
                            CharType.Alpha, CharType.Numeric, CharType.Escape, CharType.Delimiter)
                },
                {
                    State.PlainText,
                    new Transition()
                        .Add(State.PlainText, Transition.NeutralTransducer, CharType.WhiteSpace,
                            CharType.Alpha, CharType.Numeric)
                        .Add(State.Escaped, null, CharType.Escape)
                        .Add(State.Final, Transition.NeutralTransducer, CharType.Delimiter)
                },
                {
                    State.DelimitersAcc,
                    new Transition()
                        .Add(State.PlainText, Transition.NeutralTransducer, CharType.WhiteSpace,
                            CharType.Alpha, CharType.Numeric)
                        .Add(State.Escaped, null, CharType.Escape)
                        .Add(State.DelimitersAcc, Transition.NeutralTransducer, CharType.Delimiter)
                }
            },
            new HashSet<State> {State.Final, State.DelimitersAcc, State.PlainText, State.Escaped}, 
            Delimiter, '\\');

        public PlainTextToken(string content)
        {
            Content = content;
        }

        public virtual string Render(ITokenRenderer renderer) =>
            renderer.Render(this);

        public static bool TryRead(TokenReader reader, out IToken token)
        {
            token = null;
            using (reader)
            {
                var text = FsmReader.ReadToken(reader);
                if (string.IsNullOrEmpty(text)) 
                    return false;
                if (!reader.AtEnd && text.EndsWith(Delimiter.ToString()))
                {
                    text = text.Substring(0, text.Length - 1);
                    reader.RollBack(1);
                }
                reader.ReadSuccessfully = true;
                token = new PlainTextToken(text);
                return true;
            }
        }
    }
}
