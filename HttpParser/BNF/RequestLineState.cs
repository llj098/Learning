using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpParser.BNF
{
    enum RequestLineFunction
    {
        Method,
        Uri,
        None,
    }

    class RequestLine : Syntax<RequestLineFunction>
    {
        /*
         *  Request-Line   = Method SP Request-URI SP HTTP-Version CRLF 
         *  Method         =  token 
         *  Request-URI    = "*" | absoluteURI | abs_path | authority
         */

        private static SyntaxState<RequestLineFunction> s0 = AddSyntaxState(new RequestLineState(0, false, RequestLineFunction.None));
        
        protected override void Init()
        {
            InitDFAStates();
        }

        private void InitDFAStates()
        {
            var s1 = AddSyntaxState(new RequestLineState(1, false, RequestLineFunction.Method));
            var s2 = AddSyntaxState(new RequestLineState(2, false, RequestLineFunction.Method));
            var s3 = AddSyntaxState(new RequestLineState(3, false, RequestLineFunction.Method));
            var s4 = AddSyntaxState(new RequestLineState(4, true, RequestLineFunction.Method));
            var s5 = AddSyntaxState(new RequestLineState(5, true, RequestLineFunction.Method));
            var s6 = AddSyntaxState(new RequestLineState(6, true, RequestLineFunction.Method));
            var s7 = AddSyntaxState(new RequestLineState(7, true, RequestLineFunction.Method));

            s0.AddState((int)Symbols.TOKEN, s1.ID);
            s1.AddState((int)Symbols.SP, s2.ID);
            s2.AddState((int)Symbols.SP, s2.ID);
            s2.AddState((int)Symbols.TEXT, s3.ID);
            s3.AddState((int)Symbols.CRLF, s4.ID);
        }

        protected override void HandleFunction(int action, DFA<Lex.Token, RequestLineFunction> dfa)
        {
            throw new NotImplementedException();
        }
    }

    class RequestLineState : SyntaxState<RequestLineFunction>
    {
        public RequestLineState(int id, bool isQuitState, RequestLineFunction function)
        {
            ID = id;
            IsQuitState = isQuitState;
            Func = function;
        }
    
        public override void  StateFunc(int action, DFA<Lex.Token,RequestLineFunction> dfa)
        {
         	throw new NotImplementedException();
        }
    }
}
