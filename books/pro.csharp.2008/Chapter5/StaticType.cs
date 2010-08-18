using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter5
{
    class StaticType
    {
        public class Account
        {            
            public int Balance { get; set; }

            public static double Rate = 0.04;

            public Account(int balance)
            {
                Console.WriteLine("Normal Constructor Fire!");
                Balance = balance;
            }

            static Account()
            {
                Console.WriteLine("Static Constructor Fire!");
                Console.WriteLine("before assign the rate is {0}", Rate);
                Rate = 0.05;
                Console.WriteLine("so after assign the rate is {0}", Rate);
                Console.WriteLine("so static initiaizer first then the static constructor");
            }
        }
    }
}
