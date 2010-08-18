using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThreadTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestJoinInterupt t = new TestJoinInterupt();
            //t.Start();

            //Console.ReadLine();

            //WaitHandler.Go();

            MaxPoolWorkerThread.Go();

            Console.ReadLine();
        }
    }
}
