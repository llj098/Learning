using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using System.IO;
using TestsLib;

namespace Tests
{
    class AspnetHostingTest
    {
        public static void Go()
        {
            SimpleHost msh = (SimpleHost)
                ApplicationHost.CreateApplicationHost(
                    typeof(SimpleHost), "/",
                    new DirectoryInfo("../../../../LP/TestWebsite").FullName);
                    
            msh.ProcessRequest("TestHost.aspx");
            
            Console.ReadLine();
        }
    }
}
