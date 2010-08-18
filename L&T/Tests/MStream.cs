using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Tests
{
    class MStream
    {
        ///主要测试 Flush() 之后，Position 是否重置
        ///


        public static void Go()
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(Encoding.UTF8.GetBytes("123456789"), 0, Encoding.UTF8.GetBytes("123456789").Length);
            Console.WriteLine(ms.Position);
            ms.Flush();
            Console.WriteLine(ms.Position);

            Console.WriteLine(ms.Length);
            ms.Seek(0, SeekOrigin.Begin);
            ms.Write(Encoding.UTF8.GetBytes("123456789"), 0, Encoding.UTF8.GetBytes("123456789").Length);
            ms.Seek(0, SeekOrigin.Begin);
            Console.WriteLine(ms.Length);
            using (StreamReader reader = new StreamReader(ms))
            {
                Console.WriteLine(reader.ReadToEnd());
            }

            foreach (var item in ms.ToArray())
            {
                Console.Write((char)item);
            }
        }

    }
}
