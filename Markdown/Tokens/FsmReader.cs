using System.Collections.Generic;


namespace Markdown.Tokens
{
    internal enum State
    {
        Start,
        Fail,
        Alpha,
        Numeric,
        WhiteSpace,
        Escaped,
        PlainText,
        DelimitersAcc,
        Final
    }

    internal enum CharType
    {
        WhiteSpace,
        Alpha,
        Numeric,
        Escape,
        Delimiter
    }

    internal class Transition
    {
        public static readonly Transducer NeutralTransducer = c => c;
        private readonly Dictionary<CharType, State> newStatesByCharType = 
            new Dictionary<CharType, State>();
        private readonly Dictionary<CharType, Transducer> transducers = 
            new Dictionary<CharType, Transducer>();

        public delegate char Transducer(char c);

        public Transition Add(State newState, Transducer transducer, params CharType[] charTypes)
        {
            foreach (var charType in charTypes)
            {
                newStatesByCharType.Add(charType, newState);
                transducers.Add(charType, transducer);
            }
            return this;
        }
        
        internal State GetNewState(CharType charType) 
            => newStatesByCharType[charType];

        internal char? GetNewChar(CharType charType, char c) 
            => transducers[charType]?.Invoke(c);
    }
    
    internal class FsmReader
    {
        private readonly Dictionary<State, Transition> transitions;
        private readonly HashSet<State> finalStates;
        private readonly char delimiter;
        private readonly char escapeSymbol;
        

        public FsmReader(Dictionary<State, Transition> transitions, HashSet<State> finalStates,
            char delimiter, char escapeSymbol)
        {
            this.transitions = transitions;
            this.finalStates = finalStates;
            this.delimiter = delimiter;
            this.escapeSymbol = escapeSymbol;
        }

        public string ReadToken(TokenReader reader)
        {
            var state = State.Start;
            var chars = new List<char>(reader.LengthLeft);
            foreach (var currChar in reader.ReadChars())
            {
                if (state == State.Fail || state == State.Final)
                    break;
                
                var transition = transitions[state];
                var charType = GetCharType(currChar);
                state = transition.GetNewState(charType);
                var newChar = transition.GetNewChar(charType, currChar);
                if (newChar != null)
                    chars.Add((char)newChar);
            }
            return !finalStates.Contains(state) ? null : string.Join("", chars);
        }

        private CharType GetCharType(char c)
        {
            if (char.IsDigit(c))
                return CharType.Numeric;
            if (char.IsWhiteSpace(c))
                return CharType.WhiteSpace;
            if (c == escapeSymbol)
                return CharType.Escape;
            if (c == delimiter)
                return CharType.Delimiter;
            return CharType.Alpha;
        }
    }
}
