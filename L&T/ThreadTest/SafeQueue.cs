using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ThreadTest
{
    public class SafeQueue<T> : IEnumerable<T>,ICollection<T>,IEnumerable where T : class
    {
        Queue<T> _queue = new Queue<T>();
        static object _syncRoot = new object();

        public void Enqueue(T item)
        {
            lock (_syncRoot)
            {
                _queue.Enqueue(item);
                OnEnqueque(item);
            }
        }

        private void OnEnqueque(T item)
        {
            ItemEnqueued(this, System.EventArgs.Empty);
        }

        public T Dequeue()
        {
            lock (_syncRoot)
            {
                if(this._queue.Count >0)
                    return _queue.Dequeue();
                return null;
            }
        }

        public event EventHandler ItemEnqueued = delegate { };

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ICollection<T> Members

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get 
            {
                lock (_syncRoot)
                {
                    return _queue.Count;
                }
            }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<T> Members

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
