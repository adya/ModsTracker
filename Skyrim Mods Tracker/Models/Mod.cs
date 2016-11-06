using Newtonsoft.Json;
using SMT.Managers;
using System.Collections.Generic;
using SMT.JsonConverters;
using System.Linq;
using SMT.Models.PropertyInterfaces;

namespace SMT.Models
{
    class Mod : SMTNamedModel<Mod>, IStateful<ModState>, IVersioning, IValidatable, ILocalizable
    {
        private Version version;
        private Language language;
        private ModState state;

        /// <summary>
        /// Mod's version.
        /// </summary>
        public Version Version { get { return version; } set
            {
                if (version != null)
                    version.PropertyChanged -= Version_PropertyChanged;
                version = value;
                version.PropertyChanged += Version_PropertyChanged;
                OnPropertyChanged();
            }
        }

        private void Version_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged("Version");
        }

        /// <summary>
        /// Represents localization language of the mod.
        /// </summary>
        public Language Language { get { return language; } set { language = value; OnPropertyChanged(); } }

        /// <summary>
        /// Mod's state
        /// </summary>
        public ModState State { get { return state; } set { state = value; OnPropertyChanged(); } }

        [JsonIgnore]
        public string StateString { get { return State.GetDescription(); } }

        /// <summary>
        /// Sources at which this mod is available.
        /// </summary>
        [JsonConverter(typeof(SourcesIDJsonConverter))]
        public HashSet<Source> Sources { get; private set; }

        /// <summary>
        /// Checks whether the mod a has valid version or not.
        /// </summary>
        [JsonIgnore]
        public bool HasValidVersion { get { return Version != null && Version.Value.Length != 0; } }


        /// <summary>
        /// Checks whether the mod has valid configuration.
        /// </summary>
        [JsonIgnore]
        public bool IsValid { get { return HasValidName && HasValidVersion; } }

        public Mod() : base() { }

        [JsonConstructor]
        protected Mod(int id) : base(id) {}

        protected override void Init()
        {
            base.Init();
            Version = new Version();
            Sources = new HashSet<Source>();
        }

        public override void Normalize()
        {
            foreach (var src in Sources)
                src.Normalize();
        }

        public void UpdateState(bool refreshSources = true)
        {
            if (Sources == null || Sources.Count == 0 || Sources.Count(s => s.State >= SourceState.Available) == 0)
                State = ModState.NotTracking;
            else {
                if (refreshSources)
                {
                    foreach (var s in Sources)
                        s.UpdateRelativeState(this);
                }
                if (Sources.Count(s => s.State == SourceState.Updating) > 0)
                    State = ModState.Updating;
                else if (Sources.Count(s => s.State == SourceState.UpdateAvailable) > 0)
                    State = ModState.Outdated;
                else
                    State = ModState.UpToDate;
            }
        }

        public void CheckUpdate()
        {
            State = ModState.Updating;
            foreach (var src in Sources)
            {
                src.CheckUpdate();
                src.UpdateRelativeState(this);
            }
            UpdateState();
        }

        public override void CopyTo(Mod mod)
        {
            base.CopyTo(mod);
            if (mod == null) return;
            mod.Version = new Version(Version.Value);
            mod.State = State;
            mod.Language = Language;
            mod.Sources = Sources;
        }

       
    }
}
