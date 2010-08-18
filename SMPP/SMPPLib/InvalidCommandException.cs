using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMPP
{
    class InvalidCommandException:Exception
    {
        int CommandType;
    }
}
