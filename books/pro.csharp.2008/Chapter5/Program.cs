using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter5
{
    class Program
    {
        static void Main(string[] args)
        {
            ConstructorChain();
            StaticType();
            Console.ReadLine();
        }
        static void ConstructorChain()
        {
            var motor = new Chapter5.ConstructorChain.Motor(10);            
        }

        static void StaticType()
        {
            Console.WriteLine("******StaticType*******");
            var st1 = new Chapter5.StaticType.Account(1);
            var st2 = new Chapter5.StaticType.Account(2);
            Console.WriteLine("******StaticType END *******");
        }
    }


}