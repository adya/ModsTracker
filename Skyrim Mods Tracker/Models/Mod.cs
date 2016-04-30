using Newtonsoft.Json;
using System;
using SMT.Managers;
using System.Collections.Generic;
using SMT.JsonConverters;
using SMT.Utils;
using System.IO;

namespace SMT.Models
{
    class Mod : SMTNamedModel, IStateful<ModState>, IVersioning, IValidatable
    {
        private string version, root;


        /// <summary>
        /// Mod's version.
        /// </summary>
        public string Version { get { return version; } set { version = StringUtils.NonNull(value); } }

        /// <summary>
        /// Path to the file.
        /// </summary>
        public string Root { get { return root; } set { root = StringUtils.NonNull(value); } }

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

        [JsonIgnore]
        public bool HasValidRoot
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Root)) return false;
                return File.Exists(Root) || Directory.Exists(Root);
            }
        }

        /// <summary>
        /// Checks whether the mod has valid configuration.
        /// </summary>
        [JsonIgnore]
        public bool IsValid { get { return HasValidName && HasValidVersion && HasValidRoot; } }

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

        public override void Normalize()
        {
            Version = Version.Trim();
            Root = Root.Trim();
            foreach (var src in Sources)
                src.Normalize();
        }
    }
}
