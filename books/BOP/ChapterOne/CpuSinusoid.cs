using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace ChapterOne
{
    public class CpuSinusoid
    {

        public static void Go()
        {
            CpuLine();
        }
 
		private void Test()
		{
			//	
		}

        private static void CpuLine()
        {
            PerformanceCounter counter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            while (true)
            {
                if (counter.NextValue() > 50)
                {
                    Console.WriteLine("sleep");
                    Thread.Sleep(10);
                }
            }
            
        }
    }
}
