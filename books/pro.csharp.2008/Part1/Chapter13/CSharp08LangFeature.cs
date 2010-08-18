using System;

namespace ProCharp2008.Part2.Chapter13
{
    class CSharp08LangFeature
    {
        public CSharp08LangFeature(int x, int y)
        {
            Info1 = x;
            Info2 = y;
        }
        public void Do()
        {
            Console.WriteLine(Info1);
            Console.WriteLine(Info2);
        }
        public int Info1 { get; set; }
        public int Info2 { get; set; }
    }
}
