using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace ThreadTest
{

    /// <summary>
    /// 测试最大线程池工作线程
    /// </summary>
    class MaxPoolWorkerThread
    {
        delegate int f();

        static int test()
        {
            Console.WriteLine("Test begin");
            int w = 0; int io = 0; ThreadPool.GetAvailableThreads(out w, out io); Console.WriteLine("Valiable Thread, W:{0},IO:{1}", w, io);
            Thread.Sleep(1000000);
            Console.WriteLine("Test end");
            return 0;
        }

        public static void Go()
        {
            for (int i = 0; i < 500; i++)
            {
                f func = new f(test);
                func.BeginInvoke(CallBack, i);
                int w = 0; int io = 0; ThreadPool.GetAvailableThreads(out w, out io); Console.WriteLine("Valiable Thread, W:{0},IO:{1}", w, io);
            }
            return;
            //开启服务端口，等待响应
            ServerStart();

            for (int i = 0; i < 250; i++)
            {
                WebRequest request = WebRequest.Create("Http://localhost:9988");
                request.BeginGetResponse(new AsyncCallback(CallBack), i);

                int w = 0;
                int io = 0;
                ThreadPool.GetAvailableThreads(out w, out io);
                
                Console.WriteLine("Valiable Thread, W:{0},IO:{1}", w, io);
            }
        }

        private static void ServerStart()
        {
            TcpListener listener = new TcpListener(9988);
            listener.Start();
            listener.BeginAcceptSocket(new AsyncCallback(ServerCallBack),null);
        }

        private static void  ServerCallBack(IAsyncResult ar)
        {
            Console.WriteLine(String.Format("ThreadPool : {0}",Thread.CurrentThread.IsThreadPoolThread));
            //Thread.Sleep(500);
        }
    
        private static void CallBack(IAsyncResult ar)
        {
                int w = 0; int io = 0; ThreadPool.GetAvailableThreads(out w, out io); Console.WriteLine("Valiable Thread, W:{0},IO:{1}", w, io);
            Console.WriteLine("the number {0} request finish", ar.AsyncState);
        }
    }
}
