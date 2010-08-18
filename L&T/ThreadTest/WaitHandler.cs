using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadTest
{
    class WaitHandler
    {
        static SafeQueue<object> _queue = new SafeQueue<object>();
        static ManualResetEvent _event = new ManualResetEvent(false);
        static int _flag = 0;
        public static void Go()
        {
            Thread RequestThread = new Thread(new ThreadStart(Request));
            Thread ExecuteThread = new Thread(new ThreadStart(Execute));
            RequestThread.Start();
            ExecuteThread.Start();
        }

        private static void Request()
        {
            while (true)
            {
                if (_flag == 0)
                {
                    Console.WriteLine("Request Begins");
                    Thread.Sleep(2000);
                    Console.WriteLine("Request Ends");
                    Interlocked.Increment(ref _flag);
                    Interlocked.Increment(ref _flag);
                    Interlocked.Increment(ref _flag);
                    Interlocked.Increment(ref _flag);
                    Interlocked.Increment(ref _flag);
                    Interlocked.Increment(ref _flag);
                    Thread.Sleep(2000);
                    _event.Set();
                }
            }
        }

        private static void Execute()
        {
            while (true)
            {
                _event.WaitOne();

                if (_flag > 0)
                {
                    Interlocked.Decrement(ref _flag);
                    Console.WriteLine("Execute" + _flag.ToString());
                    Thread.Sleep(2000);// 为什么CPU过高？？？？
                }
                else
                    _event.Reset();
            }

        }
    }
}
