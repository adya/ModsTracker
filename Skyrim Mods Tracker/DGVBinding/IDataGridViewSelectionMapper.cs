namespace SMT.DGVBinding
{
    interface IDataGridViewIndexMapper<T> where T : new()
    {
        int ItemIndexFromRowIndex(DataGridViewBinder<T> binder, int rowIndex);
        int RowIndexFromItemIndex(DataGridViewBinder<T> binder, int itemIndex);
    }

    class SimpleIndexMapper<T> : IDataGridViewIndexMapper<T> where T : new()
    {
        public int ItemIndexFromRowIndex(DataGridViewBinder<T> binder, int rowIndex) { return rowIndex; }
        public int RowIndexFromItemIndex(DataGridViewBinder<T> binder, int itemIndex) { return itemIndex; }
    }
}
