using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using System.Web;

namespace TestsLib
{
    public class SimpleHost : MarshalByRefObject
    {
        public void ProcessRequest(string file)
        {
            SimpleWorkerRequest swr = new SimpleWorkerRequest(file, "", Console.Out);
            HttpRuntime.ProcessRequest(swr);
        }
    }
}
