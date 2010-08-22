using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpParser.BNF
{
    enum RequestHeaderFunction
    {
        FieldName,
        DoSpace,
        Content,
        Colon,
        CRLF,
        EOF,
        None,
    }

    class RequestHeaders:Tokens<RequestHeaderFunction>
    {
        /*
         *  Request-Header = *((message-header) CRLF)
         *                      CRLF
         *  message-header = field-name ":" [ field-value ]
         *  field-name     = token
         *  field-value    = *( field-content | LWS )
         *  field-content  = <the OCTETs making up the field-value
         *  and consisting of either *TEXT or combinations
         *  of token, separators, and quoted-string>
         *  text           = <any OCTET except CTLs, but including LWS>
         *  
         **************************************************************  
         *  SO IN OUR SCENE, WE USE BELOW:
         * 
         *  Request-Header = *((message-header) CRLF)
         *                      CRLF
         *  message-header = field-name ":" [ field-value ]
         *  field-name     = token
         *  field-value    = *( field-content | LWS )
         *  field-content  = *(token | sp | seperators)
         **************************************************************  
         */

        static TokensState<RequestHeaderFunction> s0 = AddSyntaxState(new RequestHeadersState(0, false, RequestHeaderFunction.None));

        protected override void Init()
        {
            InitDFAStates();
            Inited = true;
        }

        private static void InitDFAStates()
        {
            //****ATTHENTION: LWS IS NOT SUPPORTEd!!****
            var s1 = AddSyntaxState(new RequestHeadersState(1, false, RequestHeaderFunction.FieldName));
            var s2 = AddSyntaxState(new RequestHeadersState(2, false, RequestHeaderFunction.Colon));
            var s3 = AddSyntaxState(new RequestHeadersState(3, false, RequestHeaderFunction.Content));
            var s4 = AddSyntaxState(new RequestHeadersState(4, false, RequestHeaderFunction.EOF));
            //var s5 = AddSyntaxState(new RequestHeadersState(5, true, RequestHeaderFunction.None));

            s0.AddState((int)LexToken.Token, s1.ID);
            s0.AddState((int)LexToken.CRLF, s4.ID);

            s1.AddState((int)LexToken.Colon, s2.ID);

            s2.AddState((int)LexToken.Token, s2.ID);
            s2.AddState((int)LexToken.SP, s2.ID);
            s2.AddState((int)LexToken.Seperator, s2.ID);
            s2.AddState((int)LexToken.CRLF, s3.ID);

            s3.AddElseState(s0.ID);
        }
    }
    
    class RequestHeadersState:TokensState<RequestHeaderFunction>
    {
        public RequestHeadersState(int id, bool isQuitState, RequestHeaderFunction function)
        {
            ID = id;
            IsQuitState = isQuitState;
            Func = function;
        }


        public override void HandleState(int action, DFA<Lex.Token, RequestHeaderFunction> dfa)
        {
            throw new NotImplementedException();
        }
    }
}
