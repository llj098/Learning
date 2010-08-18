using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpParser.BNF
{
    class RequestHeadersBNF
    {
        /*
         *  Request-Header = message-header
         *  message-header = field-name ":" [ field-value ]
         *  field-name     = token
         *  field-value    = *( field-content | LWS )
         *  field-content  = <the OCTETs making up the field-value
         *  and consisting of either *TEXT or combinations
         *  of token, separators, and quoted-string>
         * 
         */
    }
}
