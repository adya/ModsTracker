using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMT.Models;
using System.Windows.Media;

namespace SMT.ViewModels
{
    class ModViewModel : BaseViewModel
    {
        private Mod mod;

        public ModViewModel(Mod mod)
        {
            this.mod = mod;
        }

        public string Name { get { return mod.Name; } set { mod.Name = value; OnPropertyChanged("Name"); } }
        public string Version { get { return mod.Version.Value; } set { mod.Version.Value = value; OnPropertyChanged("Version"); } }
        public Language Language { get { return mod.Language; } set { mod.Language = value; OnPropertyChanged("Language"); } }

        public Language[] AvailableLanguages { get { return new Language[] { Models.Language.Russian, Models.Language.English, Models.Language.None }; } }
    }
}
