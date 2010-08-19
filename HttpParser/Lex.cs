using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpParser
{
        /*
         * Basic Rule:
         * OCTET          = <any 8-bit sequence of data>
         * CHAR           = <any US-ASCII character (octets 0 - 127)>
         * UPALPHA        = <any US-ASCII uppercase letter "A".."Z">
         * LOALPHA        = <any US-ASCII lowercase letter "a".."z">
         * ALPHA          = UPALPHA | LOALPHA
         * DIGIT          = <any US-ASCII digit "0".."9">
         * CTL            = <any US-ASCII control character
                            (octets 0 - 31) and DEL (127)>
         * CR             = <US-ASCII CR, carriage return (13)>
         * LF             = <US-ASCII LF, linefeed (10)>
         * SP             = <US-ASCII SP, space (32)>
         * HT             = <US-ASCII HT, horizontal-tab (9)>
         * <">            = <US-ASCII double-quote mark (34)>
         * CRLF           = CR LF
         * LWS            = [CRLF] 1*( SP | HT )
         * TEXT           = <any OCTET except CTLs,
                            but including LWS>
         * HEX            = "A" | "B" | "C" | "D" | "E" | "F"
                          | "a" | "b" | "c" | "d" | "e" | "f" | DIGIT
         * token          = 1*<any CHAR except CTLs or separators>
         * separators     = "(" | ")" | "<" | ">" | "@"
                          | "," | ";" | ":" | "\" | <">
                          | "/" | "[" | "]" | "?" | "="
                          | "{" | "}" | SP | HT
         * comment        = "(" *( ctext | quoted-pair | comment ) ")"
         * ctext          = <any TEXT excluding "(" and ")">
         * quoted-string  = ( <"> *(qdtext | quoted-pair ) <"> )
         * qdtext         = <any TEXT except <">>
         * quoted-pair    = "\" CHAR
         * 
         */
	enum LexFunc
	{
		None = 0,
        OutputIdentifier = 1,//token in protocol
        DoSpace = 2,
        OutputSpace = 3,
        OutputNumeric = 4,
        OutputComment = 5,
        OutputArithmeticOpr = 6,
        DoubleQuotation = 7,
        OutputSeperator = 8,
        DoCRLF =10,
        OutputCRLF = 11,
        OutputSepartors = 12,
        DoLWS = 13,
        OutputLWS = 14,
	}
	
    class Lex:DFA<int,LexFunc>
    {
        public Lex(string input)
        {
            Input = input;
        }

        public class Token
        {
            public Symbols SymbolType;
            public string Text;
        }
        public int BeginIndex = 0;
        public string Input;
        public Token OutputToken;
        public LexFunc CurrentFunc;
        
        private static DFAState<int, LexFunc> s0 = AddState(new LexState(0));
        protected override void Init()
        {
            InitDFAStates();
            Inited = true;
        }

        private static void InitDFAStates()
        {
            InitTokenStates();
            InitTokenStates();
            InitCRLFLWSStates();
            InitSeperatorStates();
        }

        private static void InitTokenStates()
        {
            DFAState<int, LexFunc> s1 = AddState(new LexState(1));
            DFAState<int, LexFunc> s2 = AddState(new LexState(2, true, LexFunc.OutputIdentifier, null));
            s0.AddState('a', 'z', s1.ID);
            s0.AddState('A', 'Z', s1.ID);

            s1.AddState('a', 'z', s1.ID);
            s1.AddState('A', 'Z', s1.ID);

            s1.AddElseState(s2.ID);
        }

        private static void InitSpaceStates()
        {
            DFAState<int, LexFunc> s3 = AddState(new LexState(3, false, LexFunc.DoSpace, null));
            DFAState<int, LexFunc> s4 = AddState(new LexState(4, true, LexFunc.OutputSpace, null));
            //s0 [' ',\t] s3
            s0.AddState(new int[] { ' ', '\t' }, s3.ID);

            //s3 [' ',\t] s3
            s3.AddState(new int[] { ' ', '\t' }, s3.ID);
            s3.AddElseState(s4.ID);
        }

        private static void InitCRLFLWSStates()
        {
            DFAState<int, LexFunc> s5 = AddState(new LexState(5, false, LexFunc.DoCRLF, null));
            DFAState<int, LexFunc> s6 = AddState(new LexState(6, false, LexFunc.DoLWS, null));
            DFAState<int, LexFunc> s7 = AddState(new LexState(7, true, LexFunc.OutputCRLF, null));
            DFAState<int, LexFunc> s8 = AddState(new LexState(8, true, LexFunc.OutputLWS, null));

            s0.AddState(new int[] { '\r', '\n' }, s5.ID);
            s5.AddState(new int[] { ' ', '\t' }, s6.ID);
            s6.AddState(new int[] { ' ', '\t' }, s6.ID);

            s5.AddElseState(s7.ID);
            s7.AddElseState(s8.ID);
        }

        private static void InitSeperatorStates()
        {
            DFAState<int, LexFunc> s9 = AddState(new LexState(9, true, LexFunc.OutputSepartors, null));
            s0.AddState(':', s9.ID);

            DFAState<int, LexFunc> s10 = AddState(new LexState(10, true, LexFunc.OutputSepartors, null));
            s0.AddState('/', s10.ID);
        }

        protected override void HandleFunction(int action, DFA<int, LexFunc> dfa)
        {
            
        }

        private void GetString(Lex lex)
        {
            
        }
    }
	
	class LexState:DFAState<int,LexFunc>
	{
        public LexState(int id):this(id,false,LexFunc.None,null)
        {
        }
		public LexState(int id, bool isQuit, LexFunc function, IDictionary<int, int> nextStateIdDict)
        {
        	ID = id;
            IsQuitState = isQuit;
            Func = function;

            NoFunction = Func == LexFunc.None;
            if (nextStateIdDict != null)
                NextStates = nextStateIdDict;
            else
                NextStates = new Dictionary<int, int>();
        }

        public override void StateFunc(int action, DFA<int, LexFunc> dfa)
        {
            Lex lex = (Lex)dfa;
            switch (Func) {
                case LexFunc.None:
                    break;
                case LexFunc.OutputIdentifier:
                    if (lex.OutputToken == null)
                        lex.OutputToken = new Lex.Token();
                    lex.OutputToken.SymbolType = Symbols.TOKEN;
                    GetString(lex);
                    Console.WriteLine(lex.OutputToken.Text);
                    break;
                case LexFunc.DoSpace:
                    break;
                case LexFunc.OutputSpace:
                    break;
                case LexFunc.OutputNumeric:
                    break;
                case LexFunc.OutputComment:
                    break;
                case LexFunc.OutputArithmeticOpr:
                    break;
                case LexFunc.DoubleQuotation:
                    break;
                case LexFunc.OutputSeperator:
                    break;
                case LexFunc.OutputSepartors:
                    break;
                case LexFunc.OutputCRLF:
                    break;
                case LexFunc.OutputLWS:
                    break;
                default:
                    break;
            }
        }

        private void GetString(Lex lex)
        {
            int endIndex = lex.CurrentToken;

            lex.OutputToken.Text = lex.Input.Substring(lex.BeginIndex, endIndex - lex.BeginIndex);

            lex.BeginIndex = endIndex;
        }
    }
}
