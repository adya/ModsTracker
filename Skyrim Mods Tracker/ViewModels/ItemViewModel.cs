using SMT.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SMT.ViewModels
{
    class ItemViewModel : BaseViewModel, ISelectable
    {
        private bool isSelected;
        public bool IsSelected { get { return isSelected; } set { isSelected = value; OnPropertyChanged(); }}

        public virtual SolidColorBrush ItemBrush { get; }

        public virtual SolidColorBrush ItemDarkBrush
        {
            get { return new SolidColorBrush(ItemBrush.Color.ChangeLightness(-0.7f, false)); }
        }

        public virtual SolidColorBrush ItemLightBrush
        {
            get { return new SolidColorBrush(ItemBrush.Color.ChangeLightness(0.3f, false)); }
        }

        protected void UpdateColors()
        {
            OnPropertyChanged("ItemBrush");
            OnPropertyChanged("ItemDarkBrush");
            OnPropertyChanged("ItemLightBrush");
        }
    }
}
