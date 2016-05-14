
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.Utils
{
    public class LimitedList<T> : IEnumerable<T>
    {
        private List<T> list;


        public int Capacity { get; private set; }

        public int Count { get { return list.Count; } }

        public bool IsReadOnly { get { return false; } }

        public LimitedList(int capacity)
        {
            Capacity = capacity;
        }

        public LimitedList(IEnumerable<T> collection, int capacity) : this(capacity)
        {
            AddRange(collection);
        }

        public void Extend(int newCapacity)
        {
            if (newCapacity < 0) return;
            Capacity = newCapacity;
            if (Count > newCapacity)
            {
                for (int i = newCapacity - 1; i < Count; i++)
                    list.RemoveAt(i);
            }
        }

        private bool CheckCapacity()
        {
            bool removed = false;
            while (Count > Capacity) { list.RemoveAt(0); removed = true; }
            return removed;
        }

        public T this[int index]
        {
            get { return list[index]; }
        }

        public bool Add(T item)
        {
            list.Add(item);
            return !CheckCapacity();
        }

        public bool AddRange(IEnumerable<T> collection)
        {
            list.AddRange(collection);
            return !CheckCapacity();
        }
     

        public bool Remove(T item)
        {
            return list.Remove(item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public void RemoveRange(int fromIndex, int count)
        {
            list.RemoveRange(fromIndex, count);
        }

        public int RemoveAll(Predicate<T> match)
        {
            return list.RemoveAll(match);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
