using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SMT.Utils
{
    class DataGridEx : DataGrid
    {
        public DataGridEx()
        {
            this.SelectionChanged += SelectionChangedHandler;
        }

        private void SelectionChangedHandler(object sender, SelectionChangedEventArgs e)
        {
            this.SelectedItemsList = this.SelectedItems;
        }
        #region SelectedItemsList

        public ICollection SelectedItemsList
        {
            get { return (ICollection)GetValue(SelectedItemsListProperty); }
            set { var old = SelectedItemsList; SetValue(SelectedItemsListProperty, value); OnPropertyChanged(new DependencyPropertyChangedEventArgs(SelectedItemsListProperty, old, value)); }
        }

        public static readonly DependencyProperty SelectedItemsListProperty =
                DependencyProperty.Register("SelectedItemsList", typeof(ICollection), typeof(DataGridEx), new PropertyMetadata(null));

        #endregion
    }
}
