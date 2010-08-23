using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpParser.BNF
{
    enum RequestHeaderFunction
    {
        DoHeaderName,
        OutputHeaderName,
        DoSpace,
        OutputSpace,
        DoHeaderValue,
        OutputHeaderValue,
        Colon,
        DoCRLF,
        OutputCRLF,
        EOF,
        None,
    }

    class RequestHeaderTokens : Tokens<RequestHeaderFunction>
    {
        /*
         * *************************************************************      
         *  IN OUR SCENE, WE USE BELOW:
         * 
         *  Request-Header = *((message-header) CRLF)
         *                      CRLF
         *  message-header = field-name ":" [ field-value ]
         *  field-name     = token
         *  field-value    = *( field-content | LWS )
         *  field-content  = *(token | sp | seperators)
         **************************************************************
         */

        public RequestHeaderTokens(string content)
        {
            this.InputText = content;
        }   

        static TokensState<RequestHeaderFunction> s0 = AddSyntaxState(new RequestHeaderTokensState(0, false, RequestHeaderFunction.None));

        protected override void Init()
        {
            InitDFAStates();
            Inited = true;
        }
        private static void InitDFAStates()
        {
            //****ATTHENTION: LWS IS NOT SUPPORTEd!!****
            var s1 = AddSyntaxState(new RequestHeaderTokensState(1, false, RequestHeaderFunction.DoHeaderName));
            var s2 = AddSyntaxState(new RequestHeaderTokensState(2, true, RequestHeaderFunction.OutputHeaderName));
            var s3 = AddSyntaxState(new RequestHeaderTokensState(3, false, RequestHeaderFunction.DoHeaderValue));
            var s4 = AddSyntaxState(new RequestHeaderTokensState(4, true, RequestHeaderFunction.OutputHeaderValue));
            var s5 = AddSyntaxState(new RequestHeaderTokensState(5, true, RequestHeaderFunction.DoCRLF));
            var s6 = AddSyntaxState(new RequestHeaderTokensState(6, true, RequestHeaderFunction.OutputCRLF));
            var s7 = AddSyntaxState(new RequestHeaderTokensState(7, true, RequestHeaderFunction.EOF, true));

            //EOF
            s0.AddState('\r', s0.ID);
            s0.AddState('\n', s7.ID);
            
            //HeaderName
            for (int i = 32; i <= 255; i++) {
                if (!_seperatorArray.Contains(i) && i != 127 && i != ' ' && i != '\t') {
                    s0.AddState(i, s1.ID);
                    s1.AddState(i, s1.ID);
                }
            }

            s1.AddState(':', s2.ID);

            //HeaderValue 
            for (int i = 32; i < 255; i++) {
                if (i != 127) {
                    s2.AddState(i, s3.ID);
                    s3.AddState(i, s3.ID);
                }
            }

            s3.AddState('\r', s4.ID); 
            s4.AddState('\n', s5.ID);

            s5.AddElseState(s6.ID);
            s6.AddElseState(s0.ID);
        }
    }


    class RequestHeaderTokensState : TokensState<RequestHeaderFunction>
    {
        public RequestHeaderTokensState(int id, bool isQuitState, RequestHeaderFunction function)
            : this(id,
            isQuitState, function, false)
        {
        }

        public RequestHeaderTokensState(int id, bool isQuitState, RequestHeaderFunction function, bool isEndState)
        {
            ID = id;
            IsQuitState = isQuitState;
            Func = function;
            NextStates = new Dictionary<int, int>();
            IsEndState = isEndState;
        }

        public override void HandleState(int action, DFA<int, RequestHeaderFunction> dfa)
        {
           RequestHeaderTokens requestHeader = (RequestHeaderTokens)dfa;
            if (requestHeader.Output == null)
                requestHeader.Output = new TokensOutput();

            switch (Func) {
                case RequestHeaderFunction.DoHeaderName:
                case RequestHeaderFunction.DoSpace:
                case RequestHeaderFunction.DoHeaderValue:
                case RequestHeaderFunction.Colon:
                    break;
                case RequestHeaderFunction.OutputHeaderName:
                    requestHeader.Output.TokenType = SyntaxToken.HearerName;
                    GetString(requestHeader);
                    break;
                case RequestHeaderFunction.OutputHeaderValue:
                    requestHeader.Output.TokenType = SyntaxToken.HeadValue;
                    GetString(requestHeader);
                    break;
                case RequestHeaderFunction.OutputCRLF:
                    GetString(requestHeader);
                    break;
                case RequestHeaderFunction.EOF:
                case RequestHeaderFunction.None:
                    break;
            }
         
        }
    }

}
