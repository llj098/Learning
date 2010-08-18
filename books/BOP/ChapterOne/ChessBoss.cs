using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ChapterOne
{
    /// <summary>
    /// 象棋将帅问题
    /// </summary>
    class ChessBoss
    {
        ///编程之美，象棋将帅问题：
        ///题目要求：
        ///1.输出所有的将帅合法位置
        ///2.只能运用一个变量
        ///20100213


        ///唯一的一个变量，用于表示合法位置，以及临时变量等等 
        ///共8位，两个四位表示两个棋子的位置坐标，
        byte data = 0;

        public void Go()
        {
            /*
             * 0--1--2
             * 3--4--5
             * 6--7--8
             */

            while (true)
            {
                while (true)
                {
                    if ((((data >> 4) & 15) % 3) != ((data & 15) % 3))
                    {
                        Console.Write("{0}--{1} , ", (data >> 4) & 15, (data >> 0) & 15);
                    }

                    //Too ugly:
                    data = (byte)
                        ((byte)((GetBit(1) + 1) << 4) +
                        (byte)(data & 15));

                    if (((data >> 4) & 15) == 8)
                        break;
                }

                data++; data = GetBit(0); Console.WriteLine("data is {0} ", data);

                if (((data >> 0) & 15) == 8)
                    break;
            }

            return;
        }

        private byte GetBit(byte index)
        {
            return (byte)(data >> (4 * index) & 15);
        }
    }
}
