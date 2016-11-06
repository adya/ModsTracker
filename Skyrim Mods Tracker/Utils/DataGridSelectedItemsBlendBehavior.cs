using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace SMT.Utils
{
    class DataGridSelectedItemsBlendBehavior<T> : Behavior<DataGrid> where T : ISelectable
    {
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItems", typeof(ObservableCollection<T>),
            typeof(DataGridSelectedItemsBlendBehavior<T>),
            new FrameworkPropertyMetadata(null)
            {
                BindsTwoWayByDefault = true
            });

        public ObservableCollection<T> SelectedItems
        {
            get
            {
                return (ObservableCollection<T>)GetValue(SelectedItemProperty);
            }
            set
            {
                
                SetValue(SelectedItemProperty, value);
                
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.SelectionChanged += OnSelectionChanged;
            if (this.SelectedItems != null)
                this.SelectedItems.CollectionChanged += SelectedItems_CollectionChanged;
        }

        private void SelectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (T item in e.NewItems)
                {
                    item.IsSelected = true;
                    this.AssociatedObject.SelectedItems.Add(item);
                }
            }
            if (e.OldItems != null)
                foreach (T item in e.OldItems)
                {
                    item.IsSelected = false;
                    this.AssociatedObject.SelectedItems.Remove(item);
                }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (this.AssociatedObject != null)
                this.AssociatedObject.SelectionChanged -= OnSelectionChanged;
            if (this.SelectedItems != null)
                this.SelectedItems.CollectionChanged -= SelectedItems_CollectionChanged;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0 && this.SelectedItems != null)
            {
                foreach (T obj in e.AddedItems)
                    this.SelectedItems.Add(obj);
            }

            if (e.RemovedItems != null && e.RemovedItems.Count > 0 && this.SelectedItems != null)
            {
                foreach (T obj in e.RemovedItems)
                    this.SelectedItems.Remove(obj);
            }
        }
    }

    interface ISelectable
    {
        bool IsSelected { get; set; }
    }
}
