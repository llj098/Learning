﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    enum LexToken
    {
        CRLF, Token, Text, SP, Colon, LWS, Seperator
    }

    enum SyntaxToken
    {
        Method,Uri,Version,None,
        HearerName,HeadValue,
        Message,
    }