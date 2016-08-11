using SMT.Managers;
using SMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SMT.ViewModels
{
    class SourceItemViewModel : BaseViewModel
    {
        private Source src;
        private Mod mod;

        public SourceItemViewModel(Source src, Mod mod)
        {
            this.src = src;
            this.mod = mod;
            this.src.PropertyChanged += Src_PropertyChanged;
            this.mod.PropertyChanged += Mod_PropertyChanged;
            UpdateRelativeState();
        }

        private void UpdateRelativeState() { this.src.UpdateRelativeState(this.mod); this.mod.UpdateState(); OnPropertyChanged("ItemColor"); }

        private void Mod_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Version"))
                UpdateRelativeState();
        }

        private void Src_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
            if (e.PropertyName.Equals("Version"))
                UpdateRelativeState();
        }

        public string Server { get { return src.Server.Name; } }
        public string Version { get { return src.Version.Value; } }
        public string State { get { return src.StateString; } }
        public string Language { get { return src.Language.ToShortString(); } }
        public string Path { get { return src.Path; } }

        public Color ItemColor
        {
            get
            {
                switch (src.State)
                {
                    default:
                    case SourceState.Undefined:  return Colors.LightPink;
                    case SourceState.UnknownServer: return Colors.LightPink;
                    case SourceState.BrokenServer: return Colors.LightPink;
                    case SourceState.UnreachablePage: return Colors.LightPink;
                    case SourceState.UnavailableVersion: return Colors.LightPink;
                    case SourceState.UpToDate: return Colors.LightGreen;
                    case SourceState.UpdateAvailable: return Colors.Orange;
                    case SourceState.Outdated: return Colors.LightGray;
                }
            }
        }
    }
}
