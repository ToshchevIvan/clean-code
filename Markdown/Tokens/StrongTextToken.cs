using System.Collections.Generic;
using Markdown.Renderers;


namespace Markdown.Tokens
{
    public class StrongTextToken : CompoundToken
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
                        .Add(State.DelimitersAcc, Transition.NeutralTransducer, CharType.Delimiter)
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
                },
                {
                    State.DelimitersAcc,
                    new Transition()
                        .Add(State.Final, null, CharType.Delimiter)
                        .Add(State.WhiteSpace, Transition.NeutralTransducer, CharType.WhiteSpace)
                        .Add(State.Alpha, Transition.NeutralTransducer, CharType.Alpha)
                        .Add(State.Numeric, Transition.NeutralTransducer, CharType.Numeric)
                        .Add(State.Escaped, Transition.NeutralTransducer, CharType.Escape)
                }
            },
            new HashSet<State> {State.Final}, 
            Delimiter, '\\');

        public StrongTextToken(IReadOnlyList<IToken> tokens) : base(tokens)
        {
        }

        public override string Render(ITokenRenderer renderer)
        {
            var inner = RenderInnerTokens(renderer);
            return renderer.Strong(inner);
        }

        public static bool TryRead(TokenReader reader, out IToken token)
        {
            const int delimLength = 2;
            token = null;
            if (!reader.StartsWith(new string(Delimiter, delimLength)))
                return false;
            using (reader)
            {
                reader.Read(delimLength);
                var text = FsmReader.ReadToken(reader);
                if (string.IsNullOrEmpty(text)) 
                    return false;
                var innerReader = new TokenReader(reader, delimLength);
                var innerTokens = innerReader.ReadTokens();
                token = new StrongTextToken(innerTokens);
                reader.ReadSuccessfully = true;
                return true;
            }
        }
    }
}
