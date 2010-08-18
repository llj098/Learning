using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindbgLibrary
{
    class AboutString
    {
        static unsafe void ModifyConst()
        {
            string str = "Hello";
            fixed (char* pstr = str)
            {
                pstr[0] = 'X';
            }
            string str1 = "hello1";
            fixed (char* pstr = str1)
            {
                pstr[0] = '1';
            }
        }

        public static void Do()
        {
            ModifyConst();
            StringBuilder sb = new StringBuilder("Hel");
            sb.Append("lo");
            string str = sb.ToString();
            Console.WriteLine(str);

            switch (str)
            {
                case "Xello":
                    Console.WriteLine("string is Xello"); break;
                case "Hello":
                    Console.WriteLine("string is Hello"); break;
                default:
                    Console.WriteLine("Not Found"); break;
            }
        }
    }
}
