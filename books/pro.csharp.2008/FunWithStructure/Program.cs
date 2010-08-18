using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunWithStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            //Error!:
            /*
            Point p1;
            p1.Y = 2;
            */
            Point POK = new Point();
            POK.Display();
            Console.ReadLine();
        }
    }

    struct Point
    {
        public int X { get; set; }

        public int Y { get; set; }

        public void Increatement()
        {
            X++; Y++;
        }
        public void Decreatement()
        {
            X--; Y--;
        }
        public void Display()
        {
            Console.WriteLine("x is {0} and y is{1}", X, Y);
        }
    }
}
