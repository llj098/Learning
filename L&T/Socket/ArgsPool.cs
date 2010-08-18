using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace SocketTest
{
    class EventArgsPool
    {
        int _cap;
        object _syncRoot;
        private Stack<SocketAsyncEventArgs> _args;

        public EventArgsPool(int cap, EventHandler<SocketAsyncEventArgs> handler)
        {
            _syncRoot = new object();
            _args = new Stack<SocketAsyncEventArgs>(cap);
            for (int i = 0; i < cap; i++) {
                SocketAsyncEventArgs e = new SocketAsyncEventArgs();
                e.Completed += handler;
                byte[] buff = new byte[256];
                e.SetBuffer(buff, 0, buff.Length);
                _args.Push(e);
            }
        }

        public SocketAsyncEventArgs Pop()
        {
            lock(_syncRoot)
                return _args.Pop();
        }


        public void Push(SocketAsyncEventArgs e)
        {
            lock(_syncRoot)
                _args.Push(e);
        }
    }
}
