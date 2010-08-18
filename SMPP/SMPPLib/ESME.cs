using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMPPLib
{
    public class ESME
    {
        Queue<PDU> receivePDU = new Queue<PDU>();
       	public int currentSequence;
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
