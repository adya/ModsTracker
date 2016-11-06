using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace SMT.Utils
{
    class DataGridFocusedSelectionBehavior : Behavior<DataGrid>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.SelectionChanged += SelectionChangedHandler;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.SelectionChanged -= SelectionChangedHandler;
        }

        void SelectionChangedHandler(object sender, SelectionChangedEventArgs e)
        {
            if (AssociatedObject.SelectionMode == DataGridSelectionMode.Single && AssociatedObject.SelectedItem != null)
                AssociatedObject.ScrollIntoView(AssociatedObject.SelectedItem);
            else if (AssociatedObject.SelectedItems != null && AssociatedObject.SelectedItems.Count > 0)
                AssociatedObject.ScrollIntoView(AssociatedObject.SelectedItems[0]);
        }
    }
}
