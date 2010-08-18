using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests
{
    class ListReference
    {
        public void Do()
        {
            List<InnerData> list = new List<InnerData>();
            InnerData data1 = new InnerData();
            data1.Size = 10;
            list.Add(data1);
            Console.WriteLine("the inner data outside the list is {0}", data1.Size);
            list[0].Size = 20;
            Console.WriteLine("after the assgiment in the list then the data1 is {0}", data1.Size);
            Console.ReadLine();
        }

        public class InnerData
        {
            public int Size { get; set; }
        }
    }
}
