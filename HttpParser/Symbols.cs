using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpParser
{
    public enum Symbols
    {
        OCTET,
        CHAR,
        UPALPHA,
        LOALPHA,
        ALPHA,
        DIGIT,
        CTL,
        CR,
        LF,
        SP,
        HT,
        CRLF,
        LWS,
        TEXT,
        HEX,
        TOKEN,
        SEPARATORS,
        COMMENT,
        CTEXT,
        QUOTEDSTRING,
        QDTEXT,
        QUOTEDPAIR,
    }
}
