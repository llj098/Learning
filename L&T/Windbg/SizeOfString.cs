using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindbgExample
{
    class SizeOfString
    {
        public static void Do()
        {
            var c1 = new SomeClass();
            var c2 = new SomeClass();
            var c3 = new SomeClass();
            var c4 = new SomeClass();
            var c5 = new SomeClass();

            GC.Collect(2);

            unsafe
            {
                fixed (int* ptr1 = &c1.Field,
                    ptr2 = &c2.Field,
                    ptr3 = &c3.Field,
                    ptr4 = &c4.Field,
                    ptr5 = &c5.Field)
                {
                    Console.WriteLine("Size of c1: " + ((int)ptr2 - (int)ptr1));
                    Console.WriteLine("Size of c2: " + ((int)ptr3 - (int)ptr2));
                    Console.WriteLine("Size of c3: " + ((int)ptr4 - (int)ptr3));
                    Console.WriteLine("Size of c4: " + ((int)ptr5 - (int)ptr4));
                }
            }
        }
    }
    public class SomeClass
    {
        public int Field;
    }
    
}
