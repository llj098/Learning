using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMPP
{
    class PDU
    {
        int smLen;
        class Head
        {
            int CmdLen;
            int CmdId;
            int CmdStatus;
            int SequenceNumber;
        }

        class Body
        {

        }

        class SendBody
        {

        }

        public enum PType
        {
            
        }

        public static void Bind(ESME esme,int cmdId)
        {
            
        }

        public enum Command
        {
            BindTransmitter,
            BindTransceiver,
            BindReceiver,
        }
    }
}
