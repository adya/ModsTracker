using Newtonsoft.Json;
using SMT.Managers;
using System.Collections.Generic;
using SMT.Utils;
using System.IO;

namespace SMT.Models
{
    class Mod : SMTNamedModel, IStateful<ModState>, IVersioning, IValidatable
    {
        private string filename;
        private Version version;

        /// <summary>
        /// Mod's version.
        /// </summary>
        public string Version { get { return version.Value; } set { version.Value = value; } }

        /// <summary>
        /// Name of the mod's file.
        /// </summary>
        public string FileName { get { return filename; } set { filename = StringUtils.NonNull(value); } }

        /// <summary>
        /// Mod's state
        /// </summary>
        public ModState State { get; set; }

        [JsonIgnore]
        public string StateString { get { return State.GetDescription(); } }

        /// <summary>
        /// Sources at which this mod is available.
        /// </summary>
        public HashSet<ModSource> Sources { get; private set; }

        /// <summary>
        /// Checks whether the mod a has valid version or not.
        /// </summary>
        [JsonIgnore]
        public bool HasValidVersion { get { return Version != null; } }

        [JsonIgnore]
        public bool HasValidFileName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(FileName)) return false;
                string path = (SettingsManager.HasValidModsLocation ? Path.Combine(SettingsManager.ModsLocation, filename) : filename);
                return File.Exists(path) || Directory.Exists(path);
            }
        }

        /// <summary>
        /// Checks whether the mod has valid configuration.
        /// </summary>
        [JsonIgnore]
        public bool IsValid { get { return HasValidName && HasValidVersion && HasValidFileName; } }

        public Mod() : base() { }

        [JsonConstructor]
        protected Mod(int id) : base(id) {}

        protected override void Init()
        {
            base.Init();
            version = new Version();
            FileName = "";
            Sources = new HashSet<ModSource>();
        }

        public override void Normalize()
        {
            FileName = FileName.Trim();
            foreach (var src in Sources)
                src.Normalize();
            this.UpdateFilename();
        }
    }
}
