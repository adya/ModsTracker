using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.Utils
{
    class SelectionList<T> : IList<T> where T : new()
    {

        public delegate void SelectionChanged(SelectionList<T> sender);
        public event SelectionChanged OnSelectionChanged;

        private List<T> collection;

        public SelectionList(ICollection<T> collection)
        {
            this.collection = collection.ToList();
        }
        private int selectedIndex;
        public int SelectedIndex {
            get { return selectedIndex; }
            set { if (selectedIndex == value) return;
                  selectedIndex = value;
                  SelectedItem = (IsSelected ? collection[SelectedIndex] : default(T));
                  if (OnSelectionChanged != null) OnSelectionChanged(this);
            } }
        public T SelectedItem { get; private set; }
        public bool IsSelected { get { return (SelectedIndex >= 0 && SelectedIndex < collection.Count); } }
        public void ClearSelection() { SelectedIndex = -1; }

        private enum AdjustmentCause
        {
            Removing,
            Adding,
            Sorting
        }

        private void AdjustSelection(int index, AdjustmentCause cause)
        {
            if (!IsSelected) return;
            switch (cause)
            {
                case AdjustmentCause.Removing:
                    if (index == SelectedIndex)
                        ClearSelection();
                    else if (index < SelectedIndex)
                        SelectedIndex--;
                    break;
                case AdjustmentCause.Adding:
                    if (index <= SelectedIndex) SelectedIndex++;
                    break;
                case AdjustmentCause.Sorting:
                    SelectedIndex = collection.IndexOf(SelectedItem);
                    break;
                default:
                    break;
            }
        }

        #region IList
        public int Count { get { return collection.Count; } }
        public bool IsReadOnly { get { return false; } }
        public T this[int index] { get { return collection[index]; } set { collection[index] = value; } }
        public int IndexOf(T item) { return collection.IndexOf(item); }
        public void Insert(int index, T item) { collection.Insert(index, item); AdjustSelection(index, AdjustmentCause.Adding); }
        public void RemoveAt(int index) { collection.RemoveAt(index); AdjustSelection(index, AdjustmentCause.Removing); }
        public void Add(T item) { collection.Add(item); }
        public void Clear() { collection.Clear(); AdjustSelection(-1, AdjustmentCause.Removing); }
        public bool Contains(T item) { return collection.Contains(item); }
        public void CopyTo(T[] array, int arrayIndex) { collection.CopyTo(array, arrayIndex); }
        public bool Remove(T item) { bool res = collection.Remove(item); AdjustSelection(IndexOf(item), AdjustmentCause.Removing); return res; }
        public IEnumerator<T> GetEnumerator() { return collection.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
        #endregion

        #region List
        public T Find(Predicate<T> match) { return collection.Find(match); }
        public void Sort() { collection.Sort(); AdjustSelection(-1, AdjustmentCause.Sorting); }
        public void Sort(Comparison<T> comparison) { collection.Sort(comparison); AdjustSelection(-1, AdjustmentCause.Sorting); }
        public void Sort(IComparer<T> comparer) { collection.Sort(comparer); AdjustSelection(-1, AdjustmentCause.Sorting); }
        public void Sort(int index, int count, IComparer<T> comparer) { collection.Sort(index, count, comparer); AdjustSelection(-1, AdjustmentCause.Sorting); }
        #endregion
    }
}
