using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HttpParser.BNF;

namespace HttpParser
{
    class Program
    {
        public static void Main()
        {
            string content = @"GET HTTP://LIULIJIN.INFO HTTP/1.1
";
            //HOST:LIULIJIN.INFO ";

            //Console.WriteLine(parser.OutputRequest.Method);
             CodeTimer.Initialize();
             CodeTimer.Time("!", 100000, () =>
             {
                HttpParser parser = new HttpParser();
                parser.Go(content);
             });

            Console.ReadLine();
            
        }
        private static RequestLineTokens requestLine;
    }

    public class HttpParser
    {
        /*
        private static void InputLexToken(Lex lex,Lex.Token token)
        {
            if (requestLine == null)
                requestLine = new RequestLineTokens(lex);

            //Console.WriteLine("{0}||{1}", token.Text, token.SymbolType);
            var result = requestLine.InputToken((int)token.SymbolType, token);


            //Console.WriteLine("{0}||{1}", requestLine.OutputToken.Content, requestLine.OutputToken.SyntaxType);
            switch (requestLine.OutputToken.SyntaxType) {
                case SyntaxToken.Method:
                case SyntaxToken.Uri:
                case SyntaxToken.Version:
                    break;
            }
            return;
            switch (result) {
                case DFAResult.Continue:
                    break;
                case DFAResult.Quit:
                    Console.WriteLine(requestLine.OutputToken.Content);
                    break;
                case DFAResult.ElseQuit:
                    Console.WriteLine(requestLine.OutputToken.Content);
                    break;
                case DFAResult.End:
                    Console.WriteLine(requestLine.OutputToken.Content);
                    break;
                default:
                    break;
            }
        }
*/
        RequestLineTokens lineTokens;

        public void Go(string content)
        {
            Lex lex = new Lex(content);

            for (int i = 0; i < content.Length; i++) {
                var result = lex.InputToken(content[i], i);
                switch (result) {
                    case DFAResult.Continue:
                        continue;
                    case DFAResult.Quit:
                        InputLexToken(lex, lex.OutputToken);
                        break;
                    case DFAResult.ElseQuit:
                        InputLexToken(lex, lex.OutputToken);
                        i--;
                        break;
                    case DFAResult.End:
                        InputLexToken(lex, lex.OutputToken);
                        break;
                    default:
                        break;
                }
            }
        }

        enum ParseState
        {
            LineTokens,
            Headers,
            Message,
        }
        private ParseState State = ParseState.LineTokens;
        public RequestEntity OutputRequest = new RequestEntity();

        private void InputLexToken(Lex lex, Lex.Token token)
        {
            switch (State) {
                case ParseState.LineTokens:
                    InputLineTokens(lex, token);
                    break;
                case ParseState.Headers:
                    InputHeaderTokens(lex, token);
                    break;
                case ParseState.Message:
                    break;
                default:
                    break;
            }
            
        }

        private void InputLineTokens(Lex lex, Lex.Token token)
        {
            if (lineTokens == null)
                lineTokens = new RequestLineTokens(lex);

            var result = lineTokens.InputToken((int)lex.OutputToken.SymbolType, token);
            switch (result) {
                case DFAResult.Continue:
                    switch (lineTokens.Output.TokenType) {
                        case SyntaxToken.Method:
                            OutputRequest.Method = lineTokens.Output.Text;
                            break;
                        case SyntaxToken.Uri:
                            OutputRequest.Uri = lineTokens.Output.Text;
                            break;
                        case SyntaxToken.Version:
                            OutputRequest.Version = lineTokens.Output.Text;
                            break;
                    }
                    break;
                case DFAResult.Quit:
                case DFAResult.ElseQuit:
                case DFAResult.End:
                    State = ParseState.Headers;
                    break;
            }
        }

        private void InputHeaderTokens(Lex lex, Lex.Token token)
        {
            throw new NotImplementedException();
        }

        private void InputTokens(Lex lex, TokensOutput token)
        {

        }
    }

    public class RequestEntity
    {
        public string Method { get; set; }
        public string Version { get; set; }
        public string Uri { get; set; }
        public Dictionary<string,string> Headers { get; set; }
    }
}
