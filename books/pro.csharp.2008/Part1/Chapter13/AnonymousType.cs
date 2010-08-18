using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProCharp2008.Part2.Chapter13
{
    class AnonymousType
    {
        public static void Do()
        {
            var myCar = new { Size = 10, Price = 100 };
            Console.WriteLine("Fun with AnonymousType");
            Console.WriteLine("myCar.Size is {0}", myCar.Size);
            GetInfomations(myCar);
        }

        static void GetInfomations(object obj)
        {
            Console.WriteLine("Type is {0}", obj.GetType());
        }
    }
}
