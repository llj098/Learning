using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter6
{
    class Shadow
    {
        ChildClass c = new ChildClass();
        public Shadow()
        {
            c.SaySth();
        }
    }

    public class BaseClass
    {
        public void SaySth()
        {
            Console.WriteLine("BaseClass is here");
        }
    }

    public class ChildClass : BaseClass
    {
        //new public void SaySth()
        public void SaySth()
        {
            Console.WriteLine("Hi! this is the ChildClass!");
        }
    }
}
