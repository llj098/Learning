using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Tests
{
    class LinkListOrList
    {
        public LinkedList<int> Linklist { get; set; }
        public List<int> _List { get; set; }
        public void Do()
        {
            Console.WriteLine("the linklist:");
            Stopwatch watch = new Stopwatch();
            watch.Start();
            {
                Init();
                {
                    for (int k = 0; k < 10000; k++)
                    {
                        Linklist.Remove(k); 
                    }
                }
            }
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
            watch.Reset();
            watch.Start();
            {
                Init();
                {
                    for (int k = 0; k < 10000; k++)
                    {
                        _List.Remove(k);
                    }
                }
            }
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
            Console.ReadLine();
        }

        private void Init()
        {
            Linklist = new LinkedList<int>();
            _List = new List<int>();
            List<int> list = new List<int>();
            for (int i = 0; i < 10000; i++)
            {
                list.Add(i);
            }
            foreach (var item in list)
            {
                Linklist.AddLast(item);
                _List.Add(item);
            }
        }
        public LinkListOrList()
        {
            Init();
        }
    }
}
