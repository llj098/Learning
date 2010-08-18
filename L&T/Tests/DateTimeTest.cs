using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests
{
    class DateTimeTest
    {
        public static void TestHours()
        {
            DateTime dt = DateTime.Parse("2010-3-15 16:00");
            var dt1 = dt - DateTime.Parse("2010-3-15 14:10");
            Console.WriteLine(dt1.Hours);
        }
    }
}
