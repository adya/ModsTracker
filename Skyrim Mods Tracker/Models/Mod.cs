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
        private string version, filename;


        /// <summary>
        /// Mod's version.
        /// </summary>
        public string Version { get { return version; } set { version = StringUtils.NonNull(value); } }

        /// <summary>
        /// Name of the mod's file.
        /// </summary>
        public string FileName { get { return filename; } set { filename = StringUtils.NonNull(value); } }

        /// <summary>
        /// Mod's state
        /// </summary>
        public ModState State { get; set; }

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

        public Mod() : base() {}
        [JsonConstructor]
        protected Mod(int id) : base(id) {}

        protected override void Init()
        {
            base.Init();
            Version = "";
            FileName = "";
            Sources = new HashSet<ModSource>();
        }

        public override void Normalize()
        {
            Version = Version.Trim();
            FileName = FileName.Trim();
            foreach (var src in Sources)
                src.Normalize();
        }
    }
}
