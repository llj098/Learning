using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace DisCache
{
    class MemPool
    {
        public MemPool()
        {
            var mem = Marshal.AllocHGlobal(100);
            
        }
    }
}
