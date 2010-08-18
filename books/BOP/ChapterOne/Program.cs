using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChapterOne
{
    class Program
    {
        static void Main(string[] args)
        {
            //CpuSinusoid.Go();

            //LinkUpGame linkUpGame = new LinkUpGame(5, 5);
            //linkUpGame.Start();
            //Console.ReadLine();

            ChessBoss cb = new ChessBoss();
            cb.Go();
            Console.ReadLine();
        }
    }
}
