﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HttpParser.BNF;
using System.Collections.Specialized;

namespace HttpParser
{
    class Program
    {
        public static void Main()
        {
            string content =
@"POST    HTTP://LIULIJIN.INFO     HTTP/1.1
HOST:LIULIJIN.INFO
Cookie:s=1

xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
";
            CodeTimer.Initialize();
            CodeTimer.Time("!", 50000, () =>
            {
                HttpParser parser = new HttpParser();
                parser.Go(content);
            });

            //HttpParser parser = new HttpParser();
            //parser.Go(content);
            //Console.WriteLine(parser.OutputRequest.ToString());

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
        RequestHeaderTokens headerTokens;
        public void Go(string content)
        {
            lineTokens = new RequestLineTokens(content);
            headerTokens = new RequestHeaderTokens(content);

            for (int i = 0; i < content.Length; i++) {
                switch (State) {
                    case ParseState.LineTokens:
                        ParseLine(content[i],ref i);
                        break;
                    case ParseState.Headers:
                        ParseHeader(content[i], ref i);
                        break;
                    case ParseState.Message:
                        break;
                    default:
                        break;
                }
                /*
                var result = lineTokens.InputToken(content[i], i);

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
                 */
            }
        }

        private void ParseLine(char p, ref int i)
        {
            var result = lineTokens.InputToken(p, i);
            switch (result) {
                case DFAResult.Continue:
                    break;
                case DFAResult.Quit:
                    AssignRequestLine();
                    break;
                case DFAResult.ElseQuit:
                    AssignRequestLine();
                    i--;
                    break;
                case DFAResult.End:
                    AssignRequestLine();
                    State = ParseState.Headers;
                    headerTokens.Output = new TokensOutput();
                    headerTokens.Output.OutputIndex = i + 1;
                    break;
                default:
                    break;
            }
        }

        private void AssignRequestLine()
        {
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
        }

        private string lastHeaderName = "";
        private void ParseHeader(char p, ref int i)
        {
            var result = headerTokens.InputToken(p, i);
            switch (result) {
                case DFAResult.Continue:
                    break;
                case DFAResult.Quit:
                    AssignHeaders();
                    break;
                case DFAResult.ElseQuit:
                    AssignHeaders();
                    i--;
                    break;
                case DFAResult.End:
                    AssignHeaders();
                    State = ParseState.Message;
                    break;
            }
        }

        private void AssignHeaders()
        {
            if (OutputRequest.Headers == null)
                OutputRequest.Headers = new NameValueCollection();

            switch (headerTokens.Output.TokenType) {
                case SyntaxToken.HearerName:
                    lastHeaderName = headerTokens.Output.Text;
                    OutputRequest.Headers.Add(headerTokens.Output.Text, String.Empty);
                    break;
                case SyntaxToken.HeadValue:
                    OutputRequest.Headers[lastHeaderName] = headerTokens.Output.Text;
                    break;
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

        /*
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
                    
            switch (result) {
                case DFAResult.Continue:
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
            
        }

        private void InputTokens(Lex lex, TokensOutput token)
        {

        }
         */
    }

    public class RequestEntity
    {
        public string Method { get; set; }
        public string Version { get; set; }
        public string Uri { get; set; }
        public NameValueCollection Headers { get; set; }
    }
}
