
using System;

namespace SMPPLib
{
	public class COString
	{
		private int len =0;
		public string Val;

		public int Length
		{
			get
			{
				return this.len;
			}
		}
		public COString(int len)
		{
			this.len = len;
		}

		public COString(int len,string val):this(len)
		{
			Val = val;
		}

		public byte[] ToByte()
		{
			byte[] bytes = new byte[len];

			for(int i =0;i < len -1;i++)
			{
				if(Val.Length > i)
				{
					bytes[i] = Val[i];
				}
				else
				{
					bytes[i] = 0;
				}
			}

			bytes[len -1] = 0;

			return bytes;
		}
	}
}
