using System;

namespace HttpParser
{
    class TokensOutput
    {
        public SyntaxToken TokenType;
        public int OutputIndex;
        public string Text;
    }

    abstract class Tokens<Function> : DFA<int, Function>
    {
        public string InputText;
        protected static int[] _seperatorArray = new int[] {
                '(', ')' , '<' , '>','@' , ',',';', 
                ':' , '\\' , '"','/' , '[' , ']' , 
                '?' , '=' , '{' , '}' /* Because we use DFA here, so omit : SP , HT*/ };

        public TokensOutput Output;
        public static TokensState<Function> AddSyntaxState(TokensState<Function> state)
        {
            return (TokensState<Function>)AddState((DFAState<int, Function>) state);
        }
    }

    abstract class TokensState<Function> : DFAState<int, Function>
    {
        //TODO:NEED TO BE ACCURATE
        protected void GetString(Tokens<Function> tokens)
        {
            int begin = tokens.Output.OutputIndex;
            int end = tokens.CurrentToken;

            tokens.Output.Text = tokens.InputText.Substring(begin, end - begin);
            tokens.Output.OutputIndex = end;
        }
    }
}
