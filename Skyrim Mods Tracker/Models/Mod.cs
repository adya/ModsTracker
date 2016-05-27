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
        /// <summary>
        /// Mod's version.
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        /// Represents localization language of the mod.
        /// </summary>
        public Language Language { get; set; }

        /// <summary>
        /// Mod's state
        /// </summary>
        public ModState State { get; set; }

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
        public bool HasValidVersion { get { return Version != null; } }


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

        public void UpdateState()
        {
            if (Sources == null || Sources.Count == 0)
                State = ModState.NotTracking;
            else if (Sources.Count(s => s.State != SourceState.Available) > 0)
                State = ModState.NotTracking;
            else if (Sources.Count(s => Version.CompareTo(s.Version) == VersionComparison.Greater) > 0)
                State = ModState.Outdated;
            else
                State = ModState.UpToDate;
        }

        public void CheckUpdate()
        {
            foreach (var src in Sources)
                src.CheckUpdate();
            UpdateState();
        }

        public override void CopyTo(Mod mod)
        {
            base.CopyTo(mod);
            if (mod == null) return;
            mod.Version = Version;
            mod.State = State;
            mod.Language = Language;
            mod.Sources = mod.Sources;
        }

       
    }
}
