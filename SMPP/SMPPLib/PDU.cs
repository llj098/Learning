using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMPPLib
{
    public class PDU
    {
        int smLen;
		public PDUHead PDUHead;
		public PDUBody PDUBody;

        public enum PType { }

        public static void Bind(ESME esme,Command cmd)
        {
			PDU pdu = new PDU();
			pdu.PDUHead = new PDUHead();
			pdu.PDUHead.CmdId = (int)cmd;
			pdu.PDUHead.CmdStatus = 0;
			pdu.PDUHead.SequenceNumber = ++esme.currentSequence;
			pdu.PDUBody = new PDUBody();
			pdu.PDUBody.SystemId = new COString(16,"InternetGW");
			pdu.PDUBody.Password = new COString(9,"PASSWORD");
			pdu.PDUBody.SystemType = new COString(13,"WWW");
			pdu.PDUBody.InterfaceVersion = 1;
		}

        public enum Command
        {
            BindTransmitter = 0x00000002,
            BindTransceiver = 0x00000009,
            BindReceiver = 0x00000001,
        }
    }
	
	public class PDUHead
        {
            public int CmdLen;
            public int CmdId;
            public int CmdStatus;
            public int SequenceNumber;

			public byte[] ToByte()
			{
				//TODO
				return null;
			}
        }

    public class PDUBody { 
		public COString SystemId;
		public COString Password;
		public COString SystemType;
		public COString ServiceType;
		public COString SourceAddress;
		public int InterfaceVersion = -1;
		public byte Addr_ton = -1;
		public byte Addr_npi = -1;
		public byte DestAddrTon = -1;
		public byte DestAddrNpi = -1;
		public COString DestinationAddr;
		public COString AddressRange ;
		public byte ESMClass = -1;
		public byte ProtocolId = -1;
		public COString ScheduleDeliverTime;
		public COString ValidityPeriod;
		public byte RegisteredDelivery;
		public byte ReplaceIfPresentFlag;
		public byte DataCoding;
		public byte SMDefaultMsgId;
		public byte SMLength;
		public COString ShortMessage;
		public COString MessageId;
		public byte ESMClass;
		public byte ProtocolFlag;
		public byte DataCoding;
		public byte SMLength;


		public int GetLength()
		{
			int len = 6;

			if(SystemId != null)
			{
				len += SystemId.Length;
			}

			if(Password != null)
			{
				len += Password.Length;
			}

			if(SystemType != null)
			{
				len += SystemType.Length;
			}
			
			if(AddressRange != null)
			{
				len += AddressRange.Length;
			}
			
			return len;
		}

		public byte[] ToByte()
		{
			int len = GetLength();
			byte[] ret = new byte[len];
			
			return ret;
		}
	}

}
