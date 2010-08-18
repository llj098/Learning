using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SocketTest
{
    class SyncServer
    {
        private int _port;
        Socket listenSocket;
        IPEndPoint _ipe;
        public SyncServer(IPEndPoint ipe)
        {
            _ipe = ipe;
            listenSocket = new Socket(ipe.AddressFamily, 
                SocketType.Stream,
                ProtocolType.Tcp);
        }

        public void Start()
        {
            listenSocket.Bind(_ipe);
            listenSocket.Listen(Int32.MaxValue);
            Console.WriteLine("The sync server started!");
            DoAccpentAndReceive();
        }

        void DoAccpentAndReceive()
        {
            while (true) {
                if (listenSocket.Poll(-1, SelectMode.SelectRead)) {
                    Socket mySocket = listenSocket.Accept();
                    byte[] bytes = new byte[256];
                    mySocket.Receive(bytes);
                    Console.WriteLine(Encoding.UTF8.GetString(bytes));
                    //MessageBox.Show(Encoding.UTF8.GetString(bytes));
                    byte[] msg = Encoding.UTF8.GetBytes("This is a test S->C" + DateTime.Now.ToString());
                    mySocket.Send(msg);
                    mySocket.Shutdown(SocketShutdown.Both);
                    mySocket.Close();
                }
            }
            //if (_socket.Connected) 
            /*
            {
                Byte[] b = new Byte[256];
                listenSocket.Receive(b);
                Console.WriteLine(Encoding.ASCII.GetString(b));
                listenSocket.Send(b);
            }
             */
            //else {
                //DoAccpentAndReceive();
            //}
        }
    }
}
