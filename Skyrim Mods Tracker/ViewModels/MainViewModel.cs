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
        private ModItemViewModel selectedMod;

        private ModViewModel editMod;

        public BindingList<ModItemViewModel> Mods { get { return mods; } }
        public ModViewModel EditMod { get { return editMod; } set { editMod = value; OnPropertyChanged("EditMod"); } }
        public ModItemViewModel SelectedMod { get { return selectedMod; } set { selectedMod = value; OnPropertyChanged("SelectedMod"); } }
        public MainViewModel(ICollection<Mod> mods)
        {
            this.mods = new BindingList<ModItemViewModel>();
            int i = 1;
            foreach (Mod m in mods)
                this.mods.Add(new ModItemViewModel(i++, m));

            this.PropertyChanged += MainViewModel_PropertyChanged;
            
        }

        private void MainViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("SelectedMod"))
                EditMod = new ModViewModel(SelectedMod.Mod);
        }
    }
}
