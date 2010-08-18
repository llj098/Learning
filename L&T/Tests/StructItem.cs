using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Tests
{
    [StructLayout(LayoutKind.Sequential)]
    struct S1
    {
        public int a;
        public int b;
    }

    struct S2
    {
        public int a;
        public int b;
    }

}
