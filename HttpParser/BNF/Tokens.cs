using System;

namespace HttpParser
{
    class TokensOutput
    {
        public SyntaxToken TokenType;
        public string Text;
        public int OutputIndex;
    }
    
    abstract class Tokens<Function> : DFA<Lex.Token, Function>
    {
        public static TokensState<Function> AddSyntaxState(TokensState<Function> state)
        {
            return (TokensState<Function>)AddState((DFAState<Lex.Token, Function>) state);
        }
    }

    abstract class TokensState<Function> : DFAState<Lex.Token, Function>
    {
        
    }
}
