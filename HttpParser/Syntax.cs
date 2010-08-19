using System;

namespace HttpParser
{
    abstract class Syntax<Function> : DFA<Lex.Token, Function>
    {
        public static SyntaxState<Function> AddSyntaxState(SyntaxState<Function> state)
        {
            return (SyntaxState<Function>)AddState((DFAState<Lex.Token, Function>) state);
        }
    }

    abstract class SyntaxState<Function> : DFAState<Lex.Token, Function>
    {

    }
}
