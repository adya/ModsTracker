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
        private bool isChecking;

        private ObservableCollection<ModItemViewModel> modsViewModels;

        private ModItemViewModel selectedMod;
        private SourceItemViewModel selectedSource;

        private EditModViewModel editMod;
        private EditSourceViewModel editSource;

        private CheckAllModsCommand checkAllModsCommand;
        private SaveChangesCommand saveChangesCommand;

        private BaseCommand<ModItemViewModel> addModCommand;
        private BaseCommand<ModItemViewModel> deleteModCommand;

        private BaseCommand<SourceItemViewModel> addSourceCommand;
        private BaseCommand<SourceItemViewModel> deleteSourceCommand;

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

        public bool IsChecking { get { return isChecking; } set { isChecking = value; OnPropertyChanged(); OnPropertyChanged("CanCheck"); } }
        public bool CanCheck { get {return !IsChecking; } }
        #region Commands

        public CheckAllModsCommand CheckAllModsCommand
        {
            get { return checkAllModsCommand; }
            private set { checkAllModsCommand = value; OnPropertyChanged(); }
        }


        public SaveChangesCommand SaveChangesCommand
        {
            get { return saveChangesCommand; }
            set { saveChangesCommand = value; }
        }
        

        public BaseCommand<ModItemViewModel> AddModCommand
        {
            get { return addModCommand; }
            private set { addModCommand = value; OnPropertyChanged();
            }
        }
        public BaseCommand<ModItemViewModel> DeleteModCommand
        {
            get { return deleteModCommand; }
            private set { deleteModCommand = value; OnPropertyChanged();
            }
        }

        public BaseCommand<SourceItemViewModel> AddSourceCommand
        {
            get { return addSourceCommand; }
            private set { addSourceCommand = value; OnPropertyChanged();
            }
        }
        public BaseCommand<SourceItemViewModel> DeleteSourceCommand
        {
            get { return deleteSourceCommand; }
            private set { deleteSourceCommand = value; OnPropertyChanged(); }
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
                    DeleteModCommand.Parameter = SelectedMod;  // setting model manualy because Binding via CommanParameter runs in a wrong order causing parameter to be null at this point
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
                    DeleteSourceCommand.Parameter = SelectedSource; // setting model manualy because Binding via CommanParameter runs in a wrong order causing parameter to be null at this point
            }
        }

        public async void RunCheckMods(params ModItemViewModel[] mods)
        {
            IsChecking = true;
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

            IsChecking = false;
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
            AddModCommand = new AddModelCommand<ModItemViewModel>(Mods, new ModItemViewModel(new Mod(), this), ActionsManager);
            AddModCommand.CommandExecuted += ((cmd, param) =>
            {
                SelectedMod = param;
                Status.IsProgressVisible = false;
                Status.Status = string.Format("Added mod with ID = {0}", SelectedMod.Mod.ID);
            });

            DeleteModCommand = new DeleteModelCommand<ModItemViewModel>(Mods, ActionsManager);
            CheckAllModsCommand = new CheckAllModsCommand(this);
            SaveChangesCommand = new SaveChangesCommand();
        }

        private void InitSourcesActions()
        {
            AddSourceCommand = new AddModelCommand<SourceItemViewModel>(SelectedMod.Sources, new SourceItemViewModel(SelectedMod.Mod, this), ActionsManager);
            AddSourceCommand.CommandExecuted += ((cmd, param) => SelectedSource = param);

            DeleteSourceCommand = new DeleteModelCommand<SourceItemViewModel>(SelectedMod.Sources, ActionsManager);
        }

        public void AddModSource(string sourceURL)
        {
            Source source;
            if (SourcesManager.TryBuildModSource(sourceURL, out source) && SelectedMod != null)
            {
                AddSourceCommand.Execute(new SourceItemViewModel(source, SelectedMod.Mod, this));
            }
        }

        public void AddMod(string name, string sourceURL)
        {
            
            Mod mod = ModsManager.ModWithName(name);
            var item = Mods.FirstOrDefault(i => i.Mod.Equals(mod));
            Source source;
            if (SourcesManager.TryBuildModSource(sourceURL, out source))
            {
               
                if (mod == null)
                {
                    mod = new Mod() { Name = name };
                    item = new ModItemViewModel(mod, this);
                    AddModCommand.Execute(item);
                }
                SelectedMod = item;
                mod.Language = source.Language;

                var srcItem = Mods.SelectMany(i => i.Sources).FirstOrDefault(s => s.Source.HasValidURL && s.Source.URL == sourceURL);
                if (srcItem == null)
                    AddSourceCommand.Execute(new SourceItemViewModel(source, mod, this));
                SelectedSource = srcItem;
            }
        }

    }
}
