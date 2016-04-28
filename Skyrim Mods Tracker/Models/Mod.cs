using Newtonsoft.Json;
using System;
using SMT.Managers;
using System.Collections.Generic;
using SMT.JsonConverters;

namespace SMT.Models
{
    class Mod : SMTNamedModel, IStateful<ModState>, IVersioning, IValidatable
    {
        /// <summary>
        /// Mod's version.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Path to the file.
        /// </summary>
        public string Root { get; set; }

        /// <summary>
        /// Mod's state
        /// </summary>
        [JsonIgnore]
        public ModState State { get; set; }

        /// <summary>
        /// Sources at which this mod is available.
        /// </summary>
        public HashSet<ModSource> Sources { get; private set; }

        /// <summary>
        /// Checks whether the mod a has valid version or not.
        /// </summary>
        [JsonIgnore]
        public bool HasValidVersion { get { return !string.IsNullOrWhiteSpace(Version); } }

        /// <summary>
        /// Checks whether the mod has valid configuration.
        /// </summary>
        [JsonIgnore]
        public bool IsValid { get { return HasValidName && HasValidVersion && this.HasValidRoot(); } }

        public Mod() : base() {}
        [JsonConstructor]
        protected Mod(int id) : base(id) {}

        protected override void Init()
        {
            base.Init();
            Version = "";
            Root = "";
            Sources = new HashSet<ModSource>();
        }
    }
}
