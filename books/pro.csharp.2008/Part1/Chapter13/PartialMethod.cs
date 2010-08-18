using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProCharp2008.Part2.Chapter13
{
    class PartialMethod
    {

        public partial class CarLocator
        {
            public string Place { get; set; }

            public void DoIt()
            {
                GetNumber();
            }

            partial void GetNumber();
        }

        public partial class CarLocator
        {
            partial void GetNumber()
            {
                Console.WriteLine("The number is 2");
            }
        }
    }
}
