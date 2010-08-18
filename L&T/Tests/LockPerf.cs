using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Tests
{
    class LockPerf
    {

        private static void WithLock()
        {
            Queue<int> q = new Queue<int>();
            var obj = new Object();
            lock (obj)
                q.Enqueue(1);
        }

        private static void NoLock()
        {
            Queue<int> q = new Queue<int>();
            q.Enqueue(1);
        }

        public static void Go()
        {
            CodeTimer.Initialize();
            CodeTimer.Time("With lock", 10000, () =>
            {
                WithLock();
            });

            CodeTimer.Time("With no lock", 10000, () =>
            {
                NoLock();
            });
        }
    }
}
