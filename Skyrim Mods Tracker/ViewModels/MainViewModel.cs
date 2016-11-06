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
        private bool isLocked;
        private ICollection<Mod> mods;
        private BackgroundWorker Worker { get; set; }

        private ObservableCollection<ModItemViewModel> modsViewModels;

        private ModItemViewModel selectedMod;
        private SourceItemViewModel selectedSource;

        private EditModViewModel editMod;
        private EditSourceViewModel editSource;

        private CheckAllModsCommand checkAllMods;
        private SaveChangesCommand saveChanges;

        private BaseCommand<ModItemViewModel> addMod;
        private BaseCommand<ModItemViewModel> deleteMod;

        private BaseCommand<SourceItemViewModel> addSource;
        private BaseCommand<SourceItemViewModel> deleteSource;

        private ActionsManager ActionsManager { get; set; }

        public ObservableCollection<ModItemViewModel> Mods
        {
            get { return modsViewModels; }
            private set { modsViewModels = value; OnPropertyChanged(); }
        }

        public ModItemViewModel SelectedMod
        {
            get { return selectedMod; }
            set { selectedMod = value; OnPropertyChanged(); OnPropertyChanged("IsEditableMod"); }
        }

        public SourceItemViewModel SelectedSource
        {
            get { return selectedSource; }
            set { selectedSource = value; OnPropertyChanged(); OnPropertyChanged("IsEditableSource"); }
        }

        public EditModViewModel EditMod
        {
            get { return editMod; }
            set { editMod = value; OnPropertyChanged(); }
        }

        public EditSourceViewModel EditSource
        {
            get { return editSource; }
            set { editSource = value; OnPropertyChanged(); }
        }

        public StatusViewModel Status { get; private set; }

        private string DefaultStatus
        {
            get { return string.Format("Total mods: {0}.", Mods.Count); }
        }

        public bool IsEditableSource
        {
            get { return SelectedSource != null; }
        }

        public bool IsEditableMod
        {
            get { return SelectedMod != null; }
        }

        #region Commands

        public CheckAllModsCommand CheckAllMods
        {
            get { return checkAllMods; }
            private set { checkAllMods = value; OnPropertyChanged(); }
        }


        public SaveChangesCommand SaveChanges
        {
            get { return saveChanges; }
            set { saveChanges = value; }
        }
        

        public BaseCommand<ModItemViewModel> AddMod
        {
            get { return addMod; }
            private set { addMod = value; OnPropertyChanged();
            }
        }
        public BaseCommand<ModItemViewModel> DeleteMod
        {
            get { return deleteMod; }
            private set { deleteMod = value; OnPropertyChanged();
            }
        }

        public BaseCommand<SourceItemViewModel> AddSource
        {
            get { return addSource; }
            private set { addSource = value; OnPropertyChanged();
            }
        }
        public BaseCommand<SourceItemViewModel> DeleteSource
        {
            get { return deleteSource; }
            private set { deleteSource = value; OnPropertyChanged(); }
        }
        #endregion

        public MainViewModel(ICollection<Mod> mods)
        {
            ActionsManager = new ActionsManager(20);
            var list = new ObservableCollection<ModItemViewModel>();
            this.mods = mods;
            foreach (Mod mod in mods)
                list.Add(new ModItemViewModel(mod, this));
            Mods = list;
            this.PropertyChanged += MainViewModel_PropertyChanged;
            Mods.CollectionChanged += Mods_CollectionChanged;
            InitActions();

            Status = new StatusViewModel();
            Status.IsVisible = true;
            Status.Status = DefaultStatus;
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
            Status.Status = DefaultStatus;
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
                        list.Add(new SourceItemViewModel(src, SelectedMod.Mod, this));
                    InitSourcesActions();
                    DeleteMod.Parameter = SelectedMod;  // setting model manualy because Binding via CommanParameter runs in a wrong order causing parameter to be null at this point
                }
                else
                {
                    EditMod = null;
                }


            }
            else if (e.PropertyName.Equals("SelectedSource"))
            {
                EditSource = IsEditableSource ? new EditSourceViewModel(SelectedSource.Source) : null;
                OnPropertyChanged("IsEditableSource");
                if (IsEditableSource)
                    DeleteSource.Parameter = SelectedSource; // setting model manualy because Binding via CommanParameter runs in a wrong order causing parameter to be null at this point
            }
        }

        public async void RunCheckMods(params ModItemViewModel[] mods)
        {
           
            Status.Status = "Checking mods...";
            int totalTasks = mods.Aggregate(0, (acc, x) => acc + x.Sources.Count); // Count all tasks to be executed.
            float executed = 0;
            var progress = new Progress<Scheduler.UpdateProgress>(value =>
            {
                if (value.IsSourceCompleted) executed++;
                int percantage = (int)(executed / totalTasks * 100);
                Status.IsProgressVisible = true;
                Status.CurrentProgress = percantage;
                string format = "({1}/{2}). Checking '{0}' @ '{3}'";
                Status.Status = string.Format(format, value.Mod.Name, value.CurrentMod + 1, value.TotalMods, value.Source.URL);
            });
            await Scheduler.CheckModTask(progress, mods.Select(m => m.Mod).ToArray());

            Status.IsProgressVisible = false;
            Status.Status = DefaultStatus;
        }

        public async void RunCheckSources(Mod mod, params SourceItemViewModel[] sources)
        {
            Status.Status = "Checking mod's sources...";
            int totalTasks = sources.Length;
            float executed = 0;
            var progress = new Progress<Scheduler.UpdateProgress>(value =>
            {
                if (value.IsSourceCompleted) executed++;
                int percantage = (int)(executed / totalTasks * 100);
                Status.IsProgressVisible = true;
                Status.CurrentProgress = percantage;
                string format = "({1}/{2}). Checking '{0}' @ '{3}'...";
                Status.Status = string.Format(format, value.Mod.Name, value.CurrentSource + 1, value.TotalSources, value.Source.URL);
            });
            await Scheduler.CheckSourceTask(progress, mod, sources.Select(s => s.Source).ToArray());

            Status.IsProgressVisible = false;
            Status.Status = DefaultStatus;
        }

        private void InitActions()
        {
            AddMod = new AddModelCommand<ModItemViewModel>(Mods, new ModItemViewModel(new Mod(), this), ActionsManager);
            AddMod.CommandExecuted += ((cmd, param) =>
            {
                SelectedMod = param;
                Status.IsProgressVisible = false;
                Status.Status = string.Format("Added mod with ID = {0}", SelectedMod.Mod.ID);
            });

            DeleteMod = new DeleteModelCommand<ModItemViewModel>(Mods, ActionsManager);
            CheckAllMods = new CheckAllModsCommand(this);
            SaveChanges = new SaveChangesCommand();
        }

        private void InitSourcesActions()
        {
            AddSource = new AddModelCommand<SourceItemViewModel>(SelectedMod.Sources, new SourceItemViewModel(SelectedMod.Mod, this), ActionsManager);
            AddSource.CommandExecuted += ((cmd, param) => SelectedSource = param);

            DeleteSource = new DeleteModelCommand<SourceItemViewModel>(SelectedMod.Sources, ActionsManager);
        }

        public void AddModSource(string sourceURL)
        {
            Source source;
            if (SourcesManager.TryBuildModSource(sourceURL, out source) && SelectedMod != null)
            {
                AddSource.Execute(new SourceItemViewModel(source, SelectedMod.Mod, this));
            }
        }

    }
}
