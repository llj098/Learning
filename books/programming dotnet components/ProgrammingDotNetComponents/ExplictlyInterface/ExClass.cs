using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgrammingDotNetComponents.ExplictlyInterface
{
    public class ExClass:IExplicitlyInterface
    {
        #region IExplicitlyInterface Members

        void IExplicitlyInterface.Method1()
        {
            Console.WriteLine("the message from interface!");
        }

        #endregion
    }
    public class GetExClass
    {
        public static void Do()
        {
            //i can do nothing with ex1~
            ExClass ex1 = new ExClass();
            IExplicitlyInterface ex2 = new ExClass();
            ex2.Method1();
        }
    }
}
