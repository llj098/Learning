using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            Observer.Observer ob = new DesignPatterns.Observer.Observer();
            ob.Do();
        }
    }
}
