using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMPP
{
    class ESME
    {
        Queue<PDU> receivePDU = new Queue<PDU>();
        int currentSequence;
        public void QuerySM()
        {
        }
        public void CancelSM()
        { }
        public void SubmitSM()
        { }
        public void UnBind() { }
        public void BindRec()
        { }
        public void BindTrans()
        { }
    }
}
