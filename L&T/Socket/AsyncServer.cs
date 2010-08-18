using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace SocketTest
{
    public class MyPerfCounter
    {
        public MyPerfCounter(string cateName,string counterName)
        {
            PerformanceCounterCategory pcc;
            if (!PerformanceCounterCategory.Exists(cateName)) {
                CounterCreationDataCollection ccdc = new CounterCreationDataCollection();

            }
            /*
            PerformanceCounterCategory cate = new PerformanceCounterCategory(cateName);
            cate.CategoryType = PerformanceCounterCategoryType.MultiInstance;
            PerformanceCounter counter = new PerformanceCounter(cateName, counterName, false);
           
            counter.Increment();
             */
        }
        
    }

    class AsyncServer
    {
        Socket _listenSocket;
        SocketAsyncEventArgs _asyncArgs;
        IPEndPoint _ipe;
        EventArgsPool _argsPool;
        PerformanceCounter AccpentCounter;
        PerformanceCounter AccpentRate;
        public AsyncServer(IPEndPoint ipe)
        {
            if (!PerformanceCounterCategory.Exists("AsyncServer")) {
                CounterCreationDataCollection ccdc = new CounterCreationDataCollection();
                ccdc.Add(new CounterCreationData("AcceptCount", "AccecptCount #", PerformanceCounterType.NumberOfItems32));
                ccdc.Add(new CounterCreationData("AcceptRate", "AcceptRate #/sec", PerformanceCounterType.RateOfCountsPerSecond64));

                PerformanceCounterCategory.Create("AsyncServer", "", PerformanceCounterCategoryType.MultiInstance, ccdc);
            }

            AccpentCounter = new PerformanceCounter("AsyncServer", "AcceptCount","AcceptCount",false);
            AccpentRate = new PerformanceCounter("AsyncServer", "AcceptRate", "AcceptRate",false);

            _ipe = ipe;
            _listenSocket = new Socket(ipe.AddressFamily, 
                SocketType.Stream, 
                ProtocolType.Tcp);
            _argsPool = new EventArgsPool(5000, new EventHandler<SocketAsyncEventArgs>(IO_Comp));
        }

        public void Start()
        {
            _listenSocket.Bind(_ipe);
            _listenSocket.Listen(Int32.MaxValue);
            StartAccept(null);
            Console.WriteLine("Server started");
        }

        void StartAccept(SocketAsyncEventArgs e)
        {
            if (e == null) {
                e = new SocketAsyncEventArgs();
                byte[] b = new byte[256];
                e.SetBuffer(b, 0, b.Length);
                e.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Comp);
            }
            else {
                e.AcceptSocket = null;
            }

            _listenSocket.AcceptAsync(e);
        }

        void IO_Comp(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation) {
                case SocketAsyncOperation.Accept:
                    ProcessAccept(e);
                    break;
                case SocketAsyncOperation.Connect:
                    break;
                case SocketAsyncOperation.Disconnect:
                    break;
                case SocketAsyncOperation.None:
                    break;
                case SocketAsyncOperation.Receive:
                    ProcessReceive(e);
                    break;
                case SocketAsyncOperation.ReceiveFrom:
                    break;
                case SocketAsyncOperation.ReceiveMessageFrom:
                    break;
                case SocketAsyncOperation.Send:
                    ProcessSend(e);
                    break;
                case SocketAsyncOperation.SendPackets:
                    break;
                case SocketAsyncOperation.SendTo:
                    break;
                default:
                    break;
            }
        }

        private void ProcessAccept(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success && e.BytesTransferred > 0) {
                //Console.WriteLine(Encoding.ASCII.GetString(e.Buffer,0,e.BytesTransferred));
            }

            var rarg = _argsPool.Pop();
            rarg.UserToken = e.AcceptSocket;

            bool willRaiseEvent = e.AcceptSocket.ReceiveAsync(rarg);
            if (!willRaiseEvent) {
                ProcessReceive(rarg);
            }

            StartAccept(e);
            AccpentRate.Increment();
            AccpentCounter.Increment();
        }

        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            Socket s = e.UserToken as Socket;
            if (e.SocketError == SocketError.Success && e.BytesTransferred > 0) {
                //Console.WriteLine(Encoding.ASCII.GetString(e.Buffer,0,e.BytesTransferred));

                s.SendAsync(e);
            }
            else {
                Collect(s, e);
            }
        }

        private void Collect(Socket s, SocketAsyncEventArgs e)
        {
            try {
                s.Shutdown(SocketShutdown.Both);
                s.Close();
                _argsPool.Push(e);
            }
            catch (Exception) {

            }
        }

        private void ProcessSend(SocketAsyncEventArgs e)
        {
            Socket s = e.UserToken as Socket;
            Collect(s, e);
            return;

            /*
            if (e.SocketError == SocketError.Success && e.BytesTransferred > 0) {

                s.Shutdown(SocketShutdown.Both);
                s.Close();
                _argsPool.Push(e);
            }
            else {

            }
             */
        }

    }
}
