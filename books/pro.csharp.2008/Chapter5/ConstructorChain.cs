using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter5
{
    class ConstructorChain
    {
        public class Motor
        {
            public int Size { get; set; }
            public string Info { get; set; }

            public Motor()
            {
                //
                Console.WriteLine("here is the default constructor!");
            }

            public Motor(string info)
                : this(0, info)
            {
                Console.WriteLine("here is the string constructure!");
            }

            public Motor(int size)
                : this(size, "")
            {
                Console.WriteLine("here is the int constructure!");
            }

            public Motor(int size, string info)
            {
                Console.WriteLine("here is the both constructure!");
            }
        }
    }
}
