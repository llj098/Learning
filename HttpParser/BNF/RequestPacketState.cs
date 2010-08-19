using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpParser.BNF
{
    class RequestPacket
    {
    /*
     * A request parser
     * According to the RFC2616, the request's BNF is :
     * About BNF :
     *  http://www.w3.org/Protocols/rfc2616/rfc2616-sec2.html
        
     * 
     *      Request       = Request-Line              ; Section 5.1
                            *(( general-header        ; Section 4.5
                             | request-header         ; Section 5.3
                             | entity-header ) CRLF)  ; Section 7.1
                            CRLF
                            [ message-body ]          ; Section 4.3 
     *      Request-Line   = Method SP Request-URI SP HTTP-Version CRLF
     *      Method         = "OPTIONS"                ; Section 9.2
                      | "GET"                    ; Section 9.3
                      | "HEAD"                   ; Section 9.4
                      | "POST"                   ; Section 9.5
                      | "PUT"                    ; Section 9.6
                      | "DELETE"                 ; Section 9.7
                      | "TRACE"                  ; Section 9.8
                      | "CONNECT"                ; Section 9.9
                      | extension-method
                       extension-method = token
     * 
     * 
     * But we do this for simple:
     * 
     *      Request       = Request-Line              ; Section 5.1
                            *((message-header) CRLF)  ; Section 7.1
                            CRLF
                            [ message-body ]          ; Section 4.3 
     
     */
    
    }
}
