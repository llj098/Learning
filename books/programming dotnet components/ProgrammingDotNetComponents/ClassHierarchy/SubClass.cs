using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgrammingDotNetComponents.ClassHierarchy
{
    class SubClass:BaseClass
    {
        public override void VMethod()
        {
           // base.VMethod();
            Console.WriteLine("here in the subclass vmethod()");
        }

        public new void Method()
        {
            Console.WriteLine("here in the subclass method()");
        }
    }

    class BaseSubClass
    {
        public static void Do()
        {
            BaseClass class1 = new SubClass();
            class1.VMethod();
            class1.Method();
        }
    }
}
