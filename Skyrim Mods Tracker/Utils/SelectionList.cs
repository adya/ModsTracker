using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.Utils
{
    class SelectionList<T> : List<T> where T : new()
    {

        public delegate void SelectionChanged(SelectionList<T> sender);
        public event SelectionChanged OnSelectionChanged;

        private int selectedIndex;
        public int SelectedIndex {
            get { return selectedIndex; }
            set { if (selectedIndex == value) return;
                selectedIndex = value;
                SelectedItem = (IsSelected ? base[SelectedIndex] : default(T));
                if (OnSelectionChanged != null) OnSelectionChanged(this);
            } }
        public T SelectedItem { get; private set; }
        public bool IsSelected { get { return (SelectedIndex >= 0 && SelectedIndex < base.Count); } }

        public SelectionList() : base() { ClearSelection(); }
        public SelectionList(ICollection<T> collection) : base(collection) { ClearSelection(); }
        public SelectionList(int capacity) : base(capacity) { ClearSelection(); }

        public void ClearSelection() { SelectedIndex = -1; }
        private void AdjustSelection() { SelectedIndex = base.IndexOf(SelectedItem); }

        #region List

        public new T this[int index]
        {
            get { return base[index]; }
        }

        public new void Insert(int index, T item) { base.Insert(index, item); AdjustSelection(); }
        public new void InsertRange(int index, IEnumerable<T> collection) { base.InsertRange(index, collection); AdjustSelection(); }

        public new void Add(T item) { base.Add(item); AdjustSelection(); }
        public new void AddRange(IEnumerable<T> collection) { base.AddRange(collection); AdjustSelection(); }
        
        public new bool Remove(T item) { bool res = base.Remove(item); AdjustSelection(); return res; }
        public new void RemoveAt(int index) { base.RemoveAt(index); AdjustSelection(); }
        public new void RemoveRange(int index, int count) { base.RemoveRange(index, count); AdjustSelection(); }
        public new void RemoveAll(Predicate<T> match) { base.RemoveAll(match); AdjustSelection(); }

        public new void Clear() { base.Clear(); ClearSelection(); }

        public new void Reverse() { base.Reverse(); AdjustSelection(); }
        public new void Reverse(int index, int count) { base.Reverse(index, count); AdjustSelection(); }

        public new void Sort() { base.Sort(); AdjustSelection(); }
        public new void Sort(Comparison<T> comparison) { base.Sort(comparison); AdjustSelection(); }
        public new void Sort(IComparer<T> comparer) { base.Sort(comparer); AdjustSelection(); }
        public new void Sort(int index, int count, IComparer<T> comparer) { base.Sort(index, count, comparer); AdjustSelection(); }
        #endregion
    }
}
