using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMT.Utils
{
    class DataGridViewBinder<T> where T : new()
    {
        public delegate void TableRefreshedEvent(DataGridViewBinder<T> sender);
        public delegate void ItemSelectedEvent(DataGridViewBinder<T> sender, T item);
        public delegate void SelectionClearedEvent(DataGridViewBinder<T> sender);
        public delegate void PopulateRowEvent(DataGridViewBinder<T> sender, DataGridViewRow row, T item);
        

        public event TableRefreshedEvent TableRefreshed;
        public event ItemSelectedEvent ItemSelected;
        public event SelectionClearedEvent SelectionCleared;
        public event PopulateRowEvent PopulateRow;


        public SelectionList<T> Data { get; private set; }
        public DataGridView GridView { get; private set; }
        
        public bool IsBroken { get { return Data == null || GridView == null; } }

        public DataGridViewBinder(DataGridView gridView, SelectionList<T> data)
        {
            Data = data;
            GridView = gridView;
            if (!IsBroken)
            {
                Data.OnSelectionChanged += Data_OnSelectionChanged;
                GridView.SelectionChanged += GridView_SelectionChanged;
            }
        }

        private void GridView_SelectionChanged(object sender, EventArgs e)
        {
            if (GridView.SelectedRows.Count <= 0) return;
            SelectRow(GridView.SelectedRows[GridView.SelectedRows.Count - 1].Index);
        }

        private void Data_OnSelectionChanged(SelectionList<T> sender)
        {
            if (sender.IsSelected) { if (ItemSelected != null) ItemSelected(this, Data.SelectedItem); }
            else { if (SelectionCleared != null) SelectionCleared(this); }
        }

        public void SelectRow(int index) { if (IsBroken || index == Data.SelectedIndex) return; Data.SelectedIndex = index; GridView.ClearSelection(); if (Data.IsSelected) GridView.Rows[Data.SelectedIndex].Selected = true; }

        
        public void AddItem() { if (IsBroken) return; GridView.Rows.Add(); Data.Add(new T()); }
        public void RemoveSelected() { if (IsBroken) return; if (Data.IsSelected) RemoveAt(Data.SelectedIndex); }
        private void RemoveAt(int index) { if (IsBroken || index < 0 || index >= Data.Count) return; Data.RemoveAt(index); GridView.Rows.RemoveAt(index); }
        private void Remove(T item) { if (IsBroken || !Data.Contains(item)) return; GridView.Rows.RemoveAt(Data.IndexOf(item)); Data.Remove(item); }

        public void Refresh(bool rebuild = false)
        {
            if (IsBroken) return;
            if (rebuild) GridView.Rows.Clear();
            for (int i = 0; i < Data.Count; i++)
                RefreshRow((rebuild ? GridView.Rows.Add() : i));
            if (TableRefreshed != null) TableRefreshed(this);
        }

        private void RefreshRow(int index)
        {
            if (IsBroken) return;
            if (index < 0 || index >= Data.Count) return;
            DataGridViewRow row = GridView.Rows[index];
            T item = Data[index];
            if (PopulateRow != null) PopulateRow(this, row, item);
          
        }

    }
}
