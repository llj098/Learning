using System;
using System.Collections;
using System.Text;
using WindbgExample;

namespace SOSBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            //SizeOfString.Do();
            StringInterning.Do();
            return;
            AboutString.Do();
            Console.ReadLine();
            return;
            MatrixWorld matrix = new MatrixWorld();
            Console.WriteLine(matrix);
            Console.WriteLine("=========================================================");
            Console.WriteLine("到命令行下面，然后切换到windbg目录，执行adplus -hang -pn sosbasics.exe -o c:\\dumps");
            Console.ReadLine();
        }
    }

    public class MatrixWorld
    {
        private int generation;
        private double gold;
        private string name;
        private DateTime age;
        private Hashtable systemKey;
        private string[] leaders;
        private object previousOne;

        private Zion zion;

        public MatrixWorld()
        {
            this.generation = 6;
            this.gold = 123456789;
            this.name = "The Matrix";
            this.age = new DateTime(2099, 1, 1);
            this.systemKey = new Hashtable();

            this.systemKey.Add("Oracle", "会变脸的老女人");
            this.systemKey.Add("Architect", "很帅的酷老头儿，应该去演Gandalf");
            this.systemKey.Add("Smith", "徒为别人做嫁衣");

            this.leaders = new string[] { "第一代", "第二代", "第三代", "第四代", "第五代", "NEO", "那个印度小女孩" };
            this.previousOne = "NEO的前身，不知道是谁";

            this.zion = new Zion();
            this.zion.One = "NEO";
        }

        public int Generation { get { return this.generation; } }
        public double Gold { get { return this.gold; } set { this.gold = value; } }
        public string Name { get { return this.name; } }
        public DateTime Age { get { return this.age; } }
        public Hashtable SystemKey { get { return this.systemKey; } }
        public string[] Leaders { get { return this.leaders; } }
        public object PreviousOne { get { return this.previousOne; } }
        public Zion Zion { get { return this.zion; } }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new StringBuilder(1024);

            sb.Append(this.name); sb.Append(" time is "); sb.Append(this.age); sb.Append(System.Environment.NewLine);
            sb.Append("Gold: "); sb.Append(this.gold); sb.Append(System.Environment.NewLine);
            sb.Append("All key items in "); sb.Append(this.name); sb.Append(" listed here"); sb.Append(System.Environment.NewLine);

            IEnumerator ite = this.systemKey.Keys.GetEnumerator();
            while (ite.MoveNext())
            {
                sb.Append("\t");
                sb.Append(ite.Current.ToString());
                sb.Append(": ");
                sb.Append(this.systemKey[ite.Current]);
                sb.Append(System.Environment.NewLine);
            }

            sb.Append("历代救世主名单");
            sb.Append(System.Environment.NewLine);
            foreach (string leader in this.leaders)
            {
                sb.Append(leader);
                sb.Append(System.Environment.NewLine);
            }

            sb.Append(System.Environment.NewLine);
            sb.Append("其中，上一代救世主：");
            sb.Append(this.previousOne);

            sb.Append(System.Environment.NewLine);
            sb.Append("ZION的资料是：");
            sb.Append(this.zion);

            return sb.ToString();
        }
    }

    public class Zion
    {
        private string one;

        public string One
        {
            get { return this.one; }
            set { this.one = value; }
        }

        public override string ToString()
        {
            return this.one;
        }
    }
}