using SMT.Managers;
using SMT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.ViewModels
{
    class MainViewModel : BaseViewModel
    {

        private BindingList<ModItemViewModel> mods;
        private BindingList<SourceItemViewModel> sources;
        private ModItemViewModel selectedMod;

        private EditModViewModel editMod;

        public BindingList<ModItemViewModel> Mods { get { return mods; } private set { mods = value; OnPropertyChanged(); } }

        public BindingList<SourceItemViewModel> Sources { get { return sources; } private set { sources = value; OnPropertyChanged(); } }

        public EditModViewModel EditMod { get { return editMod; } set { editMod = value; OnPropertyChanged(); } }
        public ModItemViewModel SelectedMod { get { return selectedMod; } set { selectedMod = value; OnPropertyChanged(); } }
        public MainViewModel(ICollection<Mod> mods)
        {
            var list = new BindingList<ModItemViewModel>();
            int i = 1;
            foreach (Mod m in mods)
                list.Add(new ModItemViewModel(i++, m));
            Mods = list;
            this.PropertyChanged += MainViewModel_PropertyChanged;
            
        }

        private void MainViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("SelectedMod"))
            {
                EditMod = new EditModViewModel(SelectedMod.Mod);
                var list = new BindingList<SourceItemViewModel>();
                foreach (var src in SelectedMod.Mod.Sources)
                    list.Add(new SourceItemViewModel(src, SelectedMod.Mod));
                Sources = list;
            }
        }
    }
}
