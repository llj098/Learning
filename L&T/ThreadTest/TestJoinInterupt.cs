using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

namespace ThreadTest
{
    public class TestJoinInterupt
    {
        static SafeQueue<object> _queue = new SafeQueue<object>();
        static Timer _timer = new Timer(new TimerCallback(Enqueue), null, 1000, 3000);
        static Timer _timer1 = new Timer(new TimerCallback(TStart), null, 1000, 10);

        public TestJoinInterupt()
        {
            _queue.ItemEnqueued += new EventHandler(_queue_ItemEnqueued);
        }

        public void Start()
        {
        }

        void _queue_ItemEnqueued(object sender, EventArgs e)
        {
            //_thread.Interrupt();
            Console.WriteLine("Item Enqueued,Queue Count:" + _queue.Count);
        }

        private static void TStart(object state)
        {
            Console.WriteLine("TStart" + _queue.Count);
            
            while (_queue.Count > 0)
            {
                var obj = _queue.Dequeue();
                ThreadPool.QueueUserWorkItem(new WaitCallback(PoolWorker),obj);
            }
        }

        private static void PoolWorker(object state)
        {
            Console.WriteLine("PoolWoker,Object hash:" +state.GetHashCode());
        }

        private static void Enqueue(object state)
        {
            _queue.Enqueue(new object());
            _queue.Enqueue(new object());
        }
    }
}
