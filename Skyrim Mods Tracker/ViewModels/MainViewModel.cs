using SMT.Managers;
using SMT.Models;
using SMT.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SMT.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private ICollection<Mod> mods;

        private ObservableCollection<ModItemViewModel> modsViewModels;
        private ObservableCollection<SourceItemViewModel> sourcesViewModels;

        private ModItemViewModel selectedMod;
        private SourceItemViewModel selectedSource;

        private EditModViewModel editMod;
        private EditSourceViewModel editSource;

        private ActionsManager ActionsManager { get; set; }

        public ObservableCollection<ModItemViewModel> Mods { get { return modsViewModels; } private set { modsViewModels = value; OnPropertyChanged(); } }

        public ObservableCollection<SourceItemViewModel> Sources { get { return sourcesViewModels; } private set { sourcesViewModels = value; OnPropertyChanged(); } }

        public ModItemViewModel SelectedMod { get { return selectedMod; } set { selectedMod = value; OnPropertyChanged(); } }
        public SourceItemViewModel SelectedSource { get { return selectedSource; } set { selectedSource = value; OnPropertyChanged(); } }

        public EditModViewModel EditMod { get { return editMod; } set { editMod = value; OnPropertyChanged(); } }
        
        public EditSourceViewModel EditSource { get { return editSource; } set { editSource = value; OnPropertyChanged(); } }

        private int index;
        public int SelectedIndex { get { return index; } set { index = value; OnPropertyChanged(); } }

        public bool IsEditableSource { get { return SelectedSource != null; } }
        public bool IsEditableMod { get { return SelectedMod != null; } }

        public MainViewModel(ICollection<Mod> mods)
        {
            ActionsManager = new ActionsManager(20);
            var list = new ObservableCollection<ModItemViewModel>();
            this.mods = mods;
            foreach (Mod m in mods)
                list.Add(new ModItemViewModel(m));
            Mods = list;
            this.PropertyChanged += MainViewModel_PropertyChanged;
            Mods.CollectionChanged += Mods_CollectionChanged;
            InitModsActions();
        }

        private void Mods_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (ModItemViewModel item in e.NewItems)
                    mods.Add(item.Mod);
            }
            if (e.OldItems != null)
                foreach (ModItemViewModel item in e.OldItems)
                    mods.Remove(item.Mod);
        }

        private void Sources_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!IsEditableMod) return; // should not be the case
            if (e.NewItems != null)
                foreach (SourceItemViewModel item in e.NewItems)
                    SelectedMod.Mod.Sources.Add(item.Source);
            if (e.OldItems != null)
                foreach (SourceItemViewModel item in e.OldItems)
                    SelectedMod.Mod.Sources.Remove(item.Source);
        }

        private void MainViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("SelectedMod"))
            {
                if (IsEditableMod)
                {
                    EditMod = new EditModViewModel(SelectedMod.Mod);
                    var list = new ObservableCollection<SourceItemViewModel>();
                    foreach (var src in SelectedMod.Mod.Sources)
                        list.Add(new SourceItemViewModel(src, SelectedMod.Mod));
                    Sources = list;
                    Sources.CollectionChanged += Sources_CollectionChanged;
                    InitSourcesActions();
                }
                else
                {
                    Sources = null;
                    EditMod = null;
                }
                OnPropertyChanged("IsEditableMod");
                UpdateDeleteModCommand();
            }
            else if (e.PropertyName.Equals("SelectedSource"))
            {
                EditSource = IsEditableSource ? new EditSourceViewModel(SelectedSource.Source) : null;
                OnPropertyChanged("IsEditableSource");
                UpdateDeleteSourceCommand();
            }
        }

      

        private BaseCommand<ModItemViewModel> addMod;
        private BaseCommand<ModItemViewModel> deleteMod;

        private BaseCommand<SourceItemViewModel> addSource;
        private BaseCommand<SourceItemViewModel> deleteSource;

        private void InitModsActions()
        {
            AddMod = new AddModelCommand<ModItemViewModel>(Mods, new ModItemViewModel(), ActionsManager);
            AddMod.CommandExecuted += ((cmd, param) => { SelectedMod = param;  });

            DeleteMod = new DeleteModelCommand<ModItemViewModel>(Mods, ActionsManager);
        }
        private void InitSourcesActions()
        {
            AddSource = new AddModelCommand<SourceItemViewModel>(Sources, ActionsManager);
            AddSource.Parameter = new SourceItemViewModel(SelectedMod.Mod);
            AddSource.CommandExecuted += ((cmd, param) => SelectedSource = param);

            DeleteSource = new DeleteModelCommand<SourceItemViewModel>(Sources, ActionsManager);
        }
        private void UpdateDeleteModCommand()
        {
            DeleteMod.Parameter = SelectedMod;  // setting model manualy because Binding via CommanParameter runs in a wrong order causing parameter to be null at this point
            DeleteMod.Update();
        }

        private void UpdateDeleteSourceCommand()
        {
            DeleteSource.Parameter = SelectedSource;  // setting model manualy because Binding via CommanParameter runs in a wrong order causing parameter to be null at this point
            DeleteSource.Update();
        }

        public BaseCommand<ModItemViewModel> AddMod { get { return addMod; } private set { addMod = value; OnPropertyChanged(); } }
        public BaseCommand<ModItemViewModel> DeleteMod { get { return deleteMod; } private set { deleteMod = value; OnPropertyChanged(); } }

        public BaseCommand<SourceItemViewModel> AddSource { get { return addSource; } private set { addSource = value; OnPropertyChanged(); } }
        public BaseCommand<SourceItemViewModel> DeleteSource { get { return deleteSource; } private set { deleteSource = value; OnPropertyChanged(); } }
    }
}
