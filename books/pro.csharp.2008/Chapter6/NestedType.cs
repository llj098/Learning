using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter6
{
    class NestedType
    {
        Person p = new Person(140);
        Person.Eye e = new Person.Eye("");
    }
    public class Person
    {
        
        private int weight;
        public Person(int weight)
        {
            this.weight = weight;
            try
            {
                Console.WriteLine("I want to get the private info of Eye");
                //Console.WriteLine("TheEye.info is {0}", TheEye.info);
                Console.WriteLine("sorry ican not!");
            }
            catch (Exception)
            {
                
                
            }
        }

        public Eye TheEye { get; set; }

        public class Eye
        {
            private string info;
            public Person p = new Person(140);
            public Eye(string info)
            {
                Console.WriteLine("wow i get the private infomation!");
                Console.WriteLine("the weight is {0}", p.weight);
                this.info = info;
            }
        }
    }
}