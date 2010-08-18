using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            //ListReference lir = new ListReference();
            //lir.Do();
            //LinkListOrList lol = new LinkListOrList();
            //lol.Do();

            //MStream.Go();
            //MD5Test.MD5A();
            //MD5Test.MD5Provider();

            //DateTimeTest.TestHours();
            //AspnetHostingTest.Go();

            //CharArrayPointer.Go();
            //StructTest.Go();
            //ReflectionTest.Go();
            LockPerf.Go();
            Console.ReadLine();
        }
    }
}
