using SMT.Managers;
using SMT.Models;
using SMT.Utils;
using SMT.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace SMT.ViewModels
{
    class SourceItemViewModel : ItemViewModel
    {
        private Source src;
        private Mod mod;

        private OpenSourceCommand openSource;
        public BaseCommand<Source> OpenSource { get { if (openSource == null) openSource = new OpenSourceCommand(src); return openSource; } }

        public SourceItemViewModel(Mod mod) : this(new Source(), mod) { }
        public SourceItemViewModel(Source src, Mod mod)
        {
            this.src = src;
            this.mod = mod;
            this.src.PropertyChanged += Src_PropertyChanged;
            this.mod.PropertyChanged += Mod_PropertyChanged;
            UpdateRelativeState(false);
            OpenSource.Update();
        }

     

        public Source Source { get { return src; }}

        public string Server { get { return (src.Server != null ? src.Server.Name : null); } }
        public string Version { get { return src.Version.Value; } }
        public string State { get { return src.StateString; } }
        public string Language { get { return src.Language.ToShortString(); } }
        public string Path { get { return src.Path; } }
        public string URL { get { return src.URL; } }

        public override SolidColorBrush ItemBrush
        {
            get
            {
                switch (src.State)
                {
                    default:
                    case SourceState.Undefined: return Brushes.LightPink;
                    case SourceState.UnknownServer: return Brushes.LightPink;
                    case SourceState.BrokenServer: return Brushes.LightPink;
                    case SourceState.UnreachablePage: return Brushes.LightPink;
                    case SourceState.UnavailableVersion: return Brushes.LightPink;
                    case SourceState.UpToDate: return Brushes.LightGreen;
                    case SourceState.UpdateAvailable: return Brushes.Orange;
                    case SourceState.Outdated: return Brushes.LightGray;
                }
            }
        }

        private void UpdateRelativeState(bool currentStateOnly)
        {
            if (currentStateOnly)
                this.src.UpdateRelativeState(this.mod);
            else
                this.mod.UpdateState();
            UpdateColors();
        }

        private void Mod_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Version"))
                UpdateRelativeState(false);
        }

        private void Src_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
            if (e.PropertyName.Equals("Version"))
                UpdateRelativeState(true);
        }
    }
}
