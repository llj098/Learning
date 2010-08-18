using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProCharp2008.Part2.Chapter13;

namespace Part1
{
    class Program
    {
        static void Main(string[] args)
        {
            //ProCharp2008.Part2.Chapter8.GC gc = new ProCharp2008.Part2.Chapter8.GC();
            //gc.GetInfomations();

            //PartialMethod.CarLocator pm = new PartialMethod.CarLocator();
            //pm.DoIt();
            //wow~~:
            //CSharp08LangFeature c08 = new CSharp08LangFeature(3,4) { Info1 = 1, Info2 = 2 };
            //c08.Do();
            FunWithAnonymousType();
            Console.ReadLine();
        }
        static void FunWithAnonymousType()
        {
            AnonymousType.Do();
        }
    }
}
