using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProCharp2008.Part2.Chapter8
{
    class GC
    {

        public GC()
        {
           

        }
        public void GetInfomations()
        {
            Console.WriteLine("Fun with GC!");
            Console.WriteLine("Estimated bytes on heap:{0}", 
                System.GC.GetTotalMemory(false));

            Console.WriteLine("the generation of GC is {0}",
                System.GC.GetGeneration(this));

            System.GC.Collect();

            Console.WriteLine("after collect() the Estimated bytes on heap is {0}",
                System.GC.GetTotalMemory(false));
            Console.WriteLine("after collect() the Generation is {0}",
                System.GC.GetGeneration(this));
        }

    }
}
