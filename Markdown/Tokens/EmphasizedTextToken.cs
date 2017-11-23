using System;
using System.Collections.Generic;
using Markdown.Renderers;


namespace Markdown.Tokens
{
    public class EmphasizedTextToken : PlainTextToken
    {
        private const char Delimiter = '_';

        private static readonly FsmReader FsmReader = new FsmReader(
            new Dictionary<State, Transition>
            {
                {
                    State.Start,
                    new Transition()
                        .Add(State.Fail, null, CharType.WhiteSpace, CharType.Numeric, CharType.Delimiter)
                        .Add(State.Alpha, Transition.NeutralTransducer, CharType.Alpha)
                        .Add(State.Escaped, null, CharType.Escape)
                },
                {
                    State.Alpha,
                    new Transition()
                        .Add(State.WhiteSpace, Transition.NeutralTransducer, CharType.WhiteSpace)
                        .Add(State.Alpha, Transition.NeutralTransducer, CharType.Alpha)
                        .Add(State.Numeric, Transition.NeutralTransducer, CharType.Numeric)
                        .Add(State.Escaped, null, CharType.Escape)
                        .Add(State.Final, null, CharType.Delimiter)
                },
                {
                    State.Numeric,
                    new Transition()
                        .Add(State.WhiteSpace, Transition.NeutralTransducer, CharType.WhiteSpace)
                        .Add(State.Alpha, Transition.NeutralTransducer, CharType.Alpha, CharType.Delimiter)
                        .Add(State.Numeric, Transition.NeutralTransducer, CharType.Numeric)
                        .Add(State.Escaped, null, CharType.Escape)
                },
                {
                    State.WhiteSpace,
                    new Transition()
                        .Add(State.WhiteSpace, Transition.NeutralTransducer, CharType.WhiteSpace)
                        .Add(State.Alpha, Transition.NeutralTransducer, CharType.Alpha, CharType.Delimiter)
                        .Add(State.Numeric, Transition.NeutralTransducer, CharType.Numeric)
                        .Add(State.Escaped, null, CharType.Escape)
                },
                {
                    State.Escaped,
                    new Transition()
                        .Add(State.WhiteSpace, Transition.NeutralTransducer, CharType.WhiteSpace)
                        .Add(State.Alpha, Transition.NeutralTransducer, CharType.Alpha, CharType.Escape,
                            CharType.Delimiter)
                        .Add(State.Numeric, Transition.NeutralTransducer, CharType.Numeric)
                }
            },
            new HashSet<State> {State.Final}, 
            Delimiter, '\\');

        public EmphasizedTextToken(string content) : base(content)
        {
        }

        public override string Render(ITokenRenderer renderer) =>
            renderer.Emphasized(Content);

        public static bool TryRead(TokenReader reader, out IToken token)
        {
            token = null;
            if (!reader.StartsWith(Delimiter.ToString())) 
                return false;
            using (reader)
            {
                reader.Read(1);
                var text = FsmReader.ReadToken(reader);
                if (string.IsNullOrEmpty(text)) 
                    return false;
                Console.WriteLine(text);
                token = new EmphasizedTextToken(text);
                reader.ReadSuccessfully = true;
                return true;
            }
        }
    }
}
