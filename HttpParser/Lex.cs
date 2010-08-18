using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpParser
{
	enum LexFunc
	{
		None = 0,
        OutputIdentifier = 1,
        DoSpace = 2,
        OutputSpace = 3,
        OutputNumeric = 4,
        OutputComment = 5,
        OutputArithmeticOpr = 6,
        DoubleQuotation = 7,
        OutputString = 8,
        OutputComparisonOpr = 9,
        OutputBracketSymbol = 10,	
	}
	
    class Lex:DFA<int,LexFunc>
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
    }
	
	class LexState:DFAState<int,LexFunc>
	{
		public LexState(int id, bool isQuit, LexFunc function, IDictionary<int, int> nextStateIdDict)
        {
        	ID = id;
            IsQuitState = isQuit;
            Func = function;

            NoFunction = Func == LexFunc.None;
            NextStates = nextStateIdDict;
        }
	}
}
