using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpParser
{
    class Program
    {
        public static void Main()
        {
            string content = "abc def ghi jkl mno pqr";
            Lex lex = new Lex(content);
            for (int i = 0; i < content.Length; i++) {
                lex.InputToken(content[i], i);
            }
            Console.WriteLine(1);
            Console.ReadLine();
        }
    }
}
