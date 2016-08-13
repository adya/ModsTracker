using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMT.Models;
using System.Windows.Media;
using System.ComponentModel;
using SMT.Utils;

namespace SMT.ViewModels
{
    class ModItemViewModel : ItemViewModel
    {
        private Mod mod;

        public ModItemViewModel() : this(new Mod()) { } 
        public ModItemViewModel(Mod mod)
        {
            this.mod = mod;
            this.mod.PropertyChanged += Mod_PropertyChanged;
        }

        private void Mod_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
            if (e.PropertyName.Equals("State"))
                UpdateColors();
                
        }

        public Mod Mod { get { return mod; } }

        public string Name { get { return mod.Name; } }
        public string Version { get { return mod.Version.Value; }}
        public string Language { get { return mod.Language.ToShortString(); } }
        public string State { get { return mod.StateString; } }

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
                    default: return Brushes.White;
                }
            }
        }
    }
}
