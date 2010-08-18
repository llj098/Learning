using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgrammingDotNetComponents.ClassHierarchy
{
    class BaseClass
    {
        public virtual void VMethod()
        {
            Console.WriteLine("here in the base class vmethod()");
        }

        public void Method()
        {
            Console.WriteLine("here in the base class method()");
        }
    }


}
