using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpParser.BNF
{
    class SentenceOutput
    {
    }

    abstract class Sentence<Function>:DFA<TokensOutput,Function>
    {
    }

    abstract class SentenceState<Function> :DFAState<TokensOutput,Function>
    {
    }
}
