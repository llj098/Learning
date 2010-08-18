using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace BlogExport
{
    public class Spider
    {
        private static readonly string _url = "http://blog.ncuhome.cn/whjlaru/Logs/";

        public Spider(string savePath)
        {
            SavePath = savePath;
            ThreadPool.SetMaxThreads(100, 100);
        }
        public string SavePath { get; set; }


        public void GetInfo()
        {
            //Get Log
            for (int year = 2005; year < 2011; year++)
            {
                for (int month = 1; month < 13; month++)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(Getlog), new GetlogPara(year,month));
                }
            }
        }

        private class GetlogPara
        {
            public GetlogPara(int y, int m)
            {
                Year = y;
                Month = m;
            }
            public int Year { get; set; }
            public int Month { get; set; }
        }

        private void Getlog(object state)
        {
            GetlogPara para = state as GetlogPara;
            var u = _url + "/" + para.Year + "/" + para.Month;
            WebRequest request = WebRequest.Create(u);
            SaveResponse(request.GetResponse(),u.Replace(':','-').Replace('\\','-'));
        }

        protected void SaveResponse(WebResponse response,string url)
        {
            string filePath = Path.Combine(this.SavePath, url + ".html");
            var data = response.GetResponseStream();

            using (StreamReader reader = new StreamReader(data))
            {
                var content = reader.ReadToEnd();
                using (FileStream fs = File.Create(filePath))
                {
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        writer.Write(content);
                    }
                }
            }

        }
    }
}
