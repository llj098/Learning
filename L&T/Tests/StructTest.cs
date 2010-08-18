using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Tests
{
    class StructTest
    {
       
        public static void Go()
        {
            byte[] bytes = new  byte[100000];
            for (int i = 0; i < bytes.Length; i++) {
                bytes[i] = 1;
            }

            var ptr = Marshal.AllocHGlobal(100000);

            S1 s1 = new S1 { a = 1, b = 2 };
            S2 s2 = new S2 { a = 1, b = 2 };

            Console.WriteLine("Hang");
            Console.ReadLine();
            Console.WriteLine("{0},{1}", s1.a, s2.a);
            Console.WriteLine("{0},{1}", bytes[0], bytes[1]);
            Console.WriteLine("{0},{1}", ptr.GetHashCode(), ptr.ToString());
            Console.ReadLine();
            Console.ReadLine();
        }
    }
}
