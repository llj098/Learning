using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Tests
{
    public class CharArrayPointer
    {
        static char[] acharval = null;
        public static void Go()
        {
            var bytes = Encoding.UTF8.GetBytes("This is the content");

            if (acharval == null)
            {
                acharval = new char[16];
                for (int i = acharval.Length; --i >= 0; )
                {
                    if (i < 10)
                    {
                        acharval[i] = (char)('0' + i);
                    }
                    else
                    {
                        acharval[i] = (char)('A' + (i - 10));
                    }
                }
            }

            CodeTimer.Initialize();
            CodeTimer.Time("Nopointer", 10000, () => ToStringWithoutPointer(bytes));
            CodeTimer.Time("Withpointer", 10000, () => ToStringWithPointer(bytes));
        }

        private static void ToStringWithoutPointer(byte[] buf)
        {
            int iLen = buf.Length;
            char[] chars = new char[iLen * 2];
            int index = 0;

            foreach (var item in buf)
            {
                chars[index++] = acharval[(item & 0xf0) >> 4];
                chars[index++] = acharval[item & 0x0f];
            }

            //Console.WriteLine(new String(chars));
        }

        private static void ToStringWithPointer(byte[] buf)
        {
            int iLen = buf.Length;
            char[] chars = new char[iLen * 2];

            unsafe
            {
                fixed (char* fc = chars, fcharval = acharval)
                {
                    fixed (byte* fb = buf)
                    {
                        char* pc;
                        byte* pb;
                        pc = fc;
                        pb = fb;
                        while (--iLen >= 0)
                        {
                            *pc++ = fcharval[(*pb & 0xf0) >> 4];
                            *pc++ = fcharval[*pb & 0x0f];
                            pb++;
                        }
                    }
                } 
            }

            //Console.WriteLine(new String(chars));
        }
    }
}
