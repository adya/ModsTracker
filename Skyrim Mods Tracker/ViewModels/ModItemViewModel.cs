using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMT.Models;
using System.Windows.Media;
using System.ComponentModel;

namespace SMT.ViewModels
{
    class ModItemViewModel : BaseViewModel
    {
        private Mod mod;
        private int index;

        public ModItemViewModel(int index, Mod mod)
        {
            this.mod = mod;
            this.index = index;
            this.mod.PropertyChanged += Mod_PropertyChanged;
        }

        private void Mod_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
            if (e.PropertyName.Equals("State"))
                OnPropertyChanged("ItemColor");
        }

        public Mod Mod { get { return mod; } }
        public int Index { get { return index; } }

        public string Name { get { return mod.Name; } }
        public string Version { get { return mod.Version.Value; }}
        public string Language { get { return mod.Language.ToShortString(); } }
        public string State { get { return mod.StateString; } }

        public Color ItemColor
        {
            get
            {
                switch (mod.State)
                {
                    case ModState.Undefined:
                    case ModState.NotTracking: return Colors.LightPink;
                    case ModState.UpToDate: return Colors.LightGreen;
                    case ModState.Outdated: return Colors.LightGoldenrodYellow;
                    default: return Colors.White;
                }
            }
        }
    }
}
