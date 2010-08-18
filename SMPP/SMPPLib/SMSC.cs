using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMPPLib
{
    class SMSC
    {
        List<ESME> esmeList = new List<ESME>();
        IDictionary<ESME, Queue<PDU>> pdus = new Dictionary<ESME, Queue<PDU>>();

        private SMSC() { }

        public static SMSC Current
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void BindRecResp()
        {
        }
        public void BindTransResp()
        {
        }
        public void UnBindResp(ESME esme)
        {
        }
        public void OutBind(ESME esme)
        {
        }
        public void QuerySMResp()
        {
        }
        public void CancelSMResp()
        {
        }
        public void SubmitSMResp()
        {
        }
        public void DeliverSM() { }
        public void DataSM() { }
        
    }
}
