using SMT.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMT.DGVBinding
{
    class DataGridViewBinder<T> :IDisposable where T : new()
    {
        public delegate void TableRefreshedEvent(DataGridViewBinder<T> sender);
        public delegate void ItemSelectedEvent(DataGridViewBinder<T> sender, T item);
        public delegate void SelectionClearedEvent(DataGridViewBinder<T> sender);
        public delegate void BinderRowItemEvent(DataGridViewBinder<T> sender, DataGridViewRow row, T item);


        public delegate int GetItemIndexWithRowIndex(int rowIndex);

        public event TableRefreshedEvent TableRefreshed;
        public event ItemSelectedEvent ItemSelected;
        public event SelectionClearedEvent SelectionCleared;
        public event BinderRowItemEvent PopulateRow;

        private struct IndexPair : IEquatable<IndexPair>
        {
            public readonly int ItemIndex;
            public readonly int RowIndex;

            public IndexPair(int row, int item) { ItemIndex = item; RowIndex = row; }
            public bool Equals(IndexPair obj) { return obj.ItemIndex == ItemIndex && obj.RowIndex == RowIndex; }
            public override int GetHashCode() { return ItemIndex.GetHashCode() + RowIndex.GetHashCode(); }
            public override string ToString() { return string.Format("{R:{0}; I:{0}}", RowIndex, ItemIndex); }

            public static IndexPair Empty { get { return new IndexPair(-1, -1); } }
            public bool IsValid { get { return ItemIndex != -1 && RowIndex != -1; } }
        }

        public SelectionList<T> Data { get; private set; }
        public DataGridView GridView { get; private set; }
        public IDataGridViewIndexMapper<T> IndexMapper { get; set; }
        public IDataGridViewIndexMapper<T> DefaultIndexMapper { get { return new SimpleIndexMapper<T>(); } }
        
        public bool ClearOnClickOutside { get; set; }

        private Dictionary<int, BinderRowItemEvent> CellContentHadlers { get; set; }

       
        public bool IsBroken { get { return Data == null || GridView == null || IndexMapper == null; } }

        public DataGridViewBinder(DataGridView gridView, SelectionList<T> data)
        {
            Data = data;
            GridView = gridView;
            IndexMapper = DefaultIndexMapper;
            CellContentHadlers = new Dictionary<int, BinderRowItemEvent>();
            if (!IsBroken)
            {
                SubscribeSelectionEvents(true);
            }
        }

        public void AddCellClickHandler(int columnIndex, BinderRowItemEvent e)
        {
            CellContentHadlers.Add(columnIndex, e);
        }

        public void SelectRow(int rowIndex)
        {
            IndexPair pair = IndexPairWithRowIndex(rowIndex);
            SelectItemAt(pair);
            SelectRowAt(pair);
        }

        public void ClearSelection() { if (!IsBroken) Data.ClearSelection(); }
        public T SelectedItem { get { return (IsBroken ? default(T) : Data.SelectedItem); } }
        public DataGridViewRow SelectedRow { get { return (IsBroken ? null : (IsSelected ? GridView.Rows[IndexMapper.RowIndexFromItemIndex(this, SelectedItemIndex)] : null)); } }

        public bool IsSelected { get { return !IsBroken && Data.IsSelected; } }
        public int SelectedItemIndex { get { return (IsBroken ? SelectionList<T>.DeselectedIndex : Data.SelectedIndex); } }
        
        public T AddItem(){ return AddItem(new T()); }

        public T AddItem(T item)
        {
            if (IsBroken) return default(T);
            int rowIndex = GridView.Rows.Add();
            Data.Add(item);
            RefreshAt(new IndexPair(rowIndex, Data.Count - 1));
            return item;
            /// TODO: Consider Sorting
        }

        public void RemoveSelected()
        {
            if (IsBroken) return;
            if (Data.IsSelected) RemoveAt(IndexPairWithItemIndex(Data.SelectedIndex));
        }

        public void Refresh(bool rebuild = false)
        {
            if (IsBroken) return;
            if (rebuild)
            {
                SubscribeSelectionEvents(false); // pause selection tracking
                GridView.Rows.Clear();
            }
            for (int itemIndex = 0; itemIndex < Data.Count; itemIndex++)
            {
                if (rebuild)
                {
                    int rowIndex = GridView.Rows.Add();
                    RefreshAt(new IndexPair(rowIndex, itemIndex));
                }
                else RefreshAt(IndexPairWithItemIndex(itemIndex));
            }
            if (rebuild)
            {
                if (Data.IsSelected) SelectRowAt(IndexPairWithItemIndex(Data.SelectedIndex));
                SubscribeSelectionEvents(true); // resume selection tracking

            }
            if (TableRefreshed != null) TableRefreshed(this);
        }

        private void SelectRowAt(IndexPair pair)
        {
            if (!IsValidIndexPair(pair)) return;
            GridView.ClearSelection();
            if (Data.IsSelected) GridView.Rows[pair.RowIndex].Selected = true;
        }
        private void SelectItemAt(IndexPair pair)
        {
            if (!IsValidIndexPair(pair)) return;
            Data.SelectedIndex = pair.ItemIndex;
        }

        private void RemoveAt(IndexPair pair)
        {
            if (!IsValidIndexPair(pair)) return;
            Data.RemoveAt(pair.ItemIndex);
            GridView.Rows.RemoveAt(pair.RowIndex);
        }
      
        private void Remove(T item)
        {
            if (IsBroken || !Data.Contains(item)) return;
            RemoveAt(IndexPairWithItemIndex(Data.IndexOf(item)));
        }

        private void RefreshAt(IndexPair pair)
        {
            if (!IsValidIndexPair(pair)) return;
            DataGridViewRow row = GridView.Rows[pair.RowIndex];
            T item = Data[pair.ItemIndex];
            if (PopulateRow != null) PopulateRow(this, row, item);
        }

        private bool IsValidRowIndex(int rowIndex)
        {
            return IsValidIndexPair(IndexPairWithRowIndex(rowIndex));
        }
        private bool IsValidItemIndex(int itemIndex)
        {
            return IsValidIndexPair(IndexPairWithItemIndex(itemIndex));
        }
        private bool IsValidIndexPair(IndexPair pair)
        {
            if (IsBroken) return false;
            if (!pair.IsValid) return false;
            if (pair.RowIndex >= GridView.Rows.Count || pair.ItemIndex >= Data.Count) return false;
            return true;
        }

        private IndexPair IndexPairWithRowIndex(int rowIndex) { return new IndexPair(rowIndex, IndexMapper.ItemIndexFromRowIndex(this, rowIndex)); }
        private IndexPair IndexPairWithItemIndex(int itemIndex) { return new IndexPair(IndexMapper.RowIndexFromItemIndex(this, itemIndex), itemIndex); }

        private void SubscribeSelectionEvents(bool subscribe)
        {
            if (subscribe)
            {
                Data.OnSelectionChanged += Data_OnSelectionChanged;
                GridView.SelectionChanged += GridView_SelectionChanged;
                GridView.CellContentClick += GridView_CellContentClick;
                GridView.MouseDown += GridView_MouseDown;
            }
            else
            {
                Data.OnSelectionChanged -= Data_OnSelectionChanged;
                GridView.SelectionChanged -= GridView_SelectionChanged;
                GridView.CellContentClick -= GridView_CellContentClick;
                GridView.MouseDown -= GridView_MouseDown;
            }
        }

        private void GridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            BinderRowItemEvent handler;
            if (CellContentHadlers.TryGetValue(e.ColumnIndex, out handler))
            {
                IndexPair pair = IndexPairWithRowIndex(e.RowIndex);
                handler(this, GridView.Rows[pair.RowIndex], Data[pair.ItemIndex]);    // forward to specific column handler
            }
        }

        private void GridView_SelectionChanged(object sender, EventArgs e)
        {
            if (GridView.SelectedRows.Count <= 0) return;
            SelectItemAt(IndexPairWithRowIndex(GridView.SelectedRows[GridView.SelectedRows.Count - 1].Index));
        }

        private void GridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (ClearOnClickOutside && !IsBroken && e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hit = GridView.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.None)
                    Data.ClearSelection();
            }
        }

        private void Data_OnSelectionChanged(SelectionList<T> sender)
        {
            if (sender.IsSelected) { if (ItemSelected != null) ItemSelected(this, Data.SelectedItem); }
            else { if (SelectionCleared != null) SelectionCleared(this); }
        }

        public void Dispose()
        {
            SubscribeSelectionEvents(false);
            Data.ClearSelection();
            Data.Clear();
            GridView.ClearSelection();
            GridView.Rows.Clear();
        }
    }
}
