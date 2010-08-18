using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns.Observer
{
    // if one class changes, the observers will be notified,
    // like stock and investors
    public class Observer
    {
        public void Do()
        {
            ConcreteSubject subject = new ConcreteSubject("0.10");
            subject.Attach(new ConcreteObserver("X"));
            subject.State = "state1";
            subject.Price = "0.01";
            Console.ReadLine();
        } 
    }

    public abstract class Subject
    {
        public Subject(string price)
        {
            Observers = new List<IObserver>();
            this.price = price;
        }
        private string price;
        public string Price { 
            get{
                return price;
            }
            set { 
                price = value;
                Notify(); 
            }
        }
        public List<IObserver> Observers { get; set; }

        public void Attach(IObserver observer)
        {
            Observers.Add(observer);
        }
        public void Deattach(IObserver observer)
        {
            Observers.Remove(observer);
        }
        public void Notify()
        {
            foreach (var item in Observers)
            {
                item.Update(this);
            }
        }
    }
    public class ConcreteSubject:Subject
    {
        public ConcreteSubject(string price)
            : base(price)
        { }
        public string State { get; set; }
 
    }

    public interface IObserver
    {
        //public Subject SubjectObject { get; set; }
        string Name { get; set; }
        void Update(Subject subject);
    }

    public class ConcreteObserver:IObserver
    {
        public ConcreteObserver(string name)
        {
            Name = name;
        }
        public string Name { get; set; }


        #region IObserver Members

        public void Update(Subject subject)
        {
            Console.WriteLine("name is {0} price is {1}", Name, subject.Price);
        }

        #endregion
    }
}
