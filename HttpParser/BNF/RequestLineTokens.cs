using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpParser.BNF
{
    //TODO:Add OutputState State
    enum LineTokensFunction
    {
        DoMethod,
        OutputMethod,
        DoSpace,
        OutputSpace,
        DoUri,
        OutputUri,
        DoVersion,
        OutputVesion,
        OutputCRLF,
        EOF,
        None,
    }

    class RequestLineTokens:Tokens<LineTokensFunction>
    {
        public RequestLineTokens(string input)
        {
            InputText = input;
        }

        protected override void Init()
        {
            InitStates();
            Inited = true;
        }

        private static TokensState<LineTokensFunction> s0 = AddSyntaxState(new RequestLineTokensState(0, false, LineTokensFunction.None));
        private static void InitStates()
        {
            var s1 = AddSyntaxState(new RequestLineTokensState(1, false, LineTokensFunction.DoMethod));
            var s2 = AddSyntaxState(new RequestLineTokensState(2, true, LineTokensFunction.OutputMethod));
            var s3 = AddSyntaxState(new RequestLineTokensState(3, false, LineTokensFunction.DoSpace));
            var s4 = AddSyntaxState(new RequestLineTokensState(4, true, LineTokensFunction.OutputSpace));
            var s5 = AddSyntaxState(new RequestLineTokensState(5, false, LineTokensFunction.DoUri));
            var s6 = AddSyntaxState(new RequestLineTokensState(6, true, LineTokensFunction.OutputUri));
            var s7 = AddSyntaxState(new RequestLineTokensState(7, false, LineTokensFunction.DoSpace));
            var s8 = AddSyntaxState(new RequestLineTokensState(8, true, LineTokensFunction.OutputSpace));
            var s9 = AddSyntaxState(new RequestLineTokensState(9, false, LineTokensFunction.DoVersion));
            var s10 = AddSyntaxState(new RequestLineTokensState(10, true, LineTokensFunction.OutputVesion));
            var s11 = AddSyntaxState(new RequestLineTokensState(11, true, LineTokensFunction.EOF, true));
            //var s5 = AddSyntaxState(new RequestLineTokensState(5, false, LineTokensFunction.DoVersion));
            //var s6 = AddSyntaxState(new RequestLineTokensState(6, true, LineTokensFunction.OutputVesion,true));

            //method
            for (int i = 32; i <= 255; i++) {
                if (!_seperatorArray.Contains(i) && i != 127 && i != ' ' && i != '\t') {
                    s0.AddState(i, s1.ID);
                    s1.AddState(i, s1.ID);
                }
            }
            s1.AddState(new int[] { ' ', '\t' }, s2.ID);//output method

            //space
            s2.AddState(new int[] { ' ', '\t' }, s3.ID);//dospace
            s3.AddState(new int[] { ' ', '\t' }, s3.ID);//dospace
            s3.AddElseState(s4.ID);//output space

            //uri 
            for (int i = 32; i < 255; i++) {
                if (i != 127 && i != ' ' && i != '\t') {
                    s4.AddState(i, s5.ID);
                    s2.AddState(i, s5.ID);//douri
                    s5.AddState(i, s5.ID);//douri
                }
            }
            s5.AddState(new int[] { ' ', '\t' }, s6.ID);//output uri

            //space
            s6.AddState(new int[] { ' ', '\t' }, s7.ID);//do space
            s7.AddState(new int[] { ' ', '\t' }, s7.ID);//do space
            s7.AddElseState(s8.ID);//output space

            //version
            for (int i = 32; i < 255; i++) {
                if (i != 127 && i != ' ' && i != '\t') {
                    s8.AddState(i, s9.ID);
                    s6.AddState(i, s9.ID);
                    s9.AddState(i, s9.ID);
                }
            }

            s9.AddState('\r', s10.ID);
            s10.AddState('\n', s11.ID);
            //s5.AddState('\r', s5.ID);
            //s5.AddState('\n', s6.ID);
        }
    }

    class RequestLineTokensState : TokensState<LineTokensFunction>
    {
        public RequestLineTokensState(int id, bool isQuitState, LineTokensFunction function):this(id,
            isQuitState,function,false)
        {
        }

        public RequestLineTokensState(int id, bool isQuitState, LineTokensFunction function,bool isEndState)
        {
            ID = id;
            IsQuitState = isQuitState;
            Func = function;
            NextStates = new Dictionary<int, int>();
            IsEndState = isEndState;
        }


        public override void HandleState(int action, DFA<int, LineTokensFunction> dfa)
        {
            RequestLineTokens requestLine = (RequestLineTokens)dfa;
            if (requestLine.Output == null)
                requestLine.Output = new TokensOutput();

            switch (Func) {
                case LineTokensFunction.OutputMethod:
                    requestLine.Output.TokenType = SyntaxToken.Method;
                    GetString(requestLine);
                    break;
                case LineTokensFunction.OutputUri:
                    requestLine.Output.TokenType = SyntaxToken.Uri;
                    GetString(requestLine);
                    break;
                case LineTokensFunction.OutputVesion:
                    requestLine.Output.TokenType = SyntaxToken.Version;
                    GetString(requestLine);
                    break;
                case LineTokensFunction.OutputSpace:
                    requestLine.Output.TokenType = SyntaxToken.None;
                    GetString(requestLine);
                    break;
                case LineTokensFunction.OutputCRLF:
                case LineTokensFunction.None:
                    requestLine.Output.TokenType = SyntaxToken.None;
                    break;
                case LineTokensFunction.EOF:
                    break;
            }
        }
    }
}
