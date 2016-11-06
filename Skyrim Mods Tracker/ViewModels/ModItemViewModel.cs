using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMT.Models;
using System.Windows.Media;
using System.ComponentModel;
using SMT.Utils;
using SMT.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace SMT.ViewModels
{
    class ModItemViewModel : ItemViewModel
    {
        private Mod mod;

        private CheckModStateCommand checkMod;

        private ObservableCollection<SourceItemViewModel> sources;

        public ModItemViewModel(MainViewModel parent) : this(new Mod(), parent) { } 
        public ModItemViewModel(Mod mod, MainViewModel parent)
        {
            this.mod = mod;
            var list = new ObservableCollection<SourceItemViewModel>();
            foreach (var src in mod.Sources)
                list.Add(new SourceItemViewModel(src, mod, parent));
            this.Sources = list;
            this.Sources.CollectionChanged += Sources_CollectionChanged;
            this.mod.PropertyChanged += Mod_PropertyChanged;
            CheckState = new CheckModStateCommand(this, parent);

        }

       

        private void Mod_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
            OnPropertyChanged("IsEnabled");
            if (e.PropertyName.Equals("State"))
                UpdateColors();
        }

        private void Sources_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (SourceItemViewModel item in e.NewItems)
                    mod.Sources.Add(item.Source);
            if (e.OldItems != null)
                foreach (SourceItemViewModel item in e.OldItems)
                    mod.Sources.Remove(item.Source);
            OnPropertyChanged("IsEnabled");
        }

        public Mod Mod { get { return mod; } }

        public string Name { get { return mod.Name; } }
        public string Version { get { return mod.Version.Value; }}
        public string Language { get { return mod.Language.ToShortString(); } }
        public string State { get { return mod.StateString; } }

        public ObservableCollection<SourceItemViewModel> Sources { get { return sources; } set { sources = value; OnPropertyChanged(); } }

        public bool IsEnabled {
            get
            {
                return mod.State != ModState.Updating && 
                       mod.State != ModState.NotTracking && CheckState.CanExecute(this);
            }
        }


        public CheckModStateCommand CheckState { get { return checkMod; } private set { checkMod = value; OnPropertyChanged(); } }


        public override SolidColorBrush ItemBrush
        {
            get
            {
                switch (mod.State)
                {
                    case ModState.Undefined:
                    case ModState.NotTracking: return Brushes.LightPink;
                    case ModState.UpToDate: return Brushes.LightGreen;
                    case ModState.Outdated: return Brushes.Orange;
                    case ModState.Updating: return Brushes.LightSkyBlue;
                    default: return Brushes.White;
                }
            }
        }
    }
}
