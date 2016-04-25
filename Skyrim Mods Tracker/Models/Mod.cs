using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SMT.Models
{
    class Mod
    {
        public enum ModState { Undefined, MissedFile, NotTracking, Outdated, UpToDate }

        public int ID { get; private set; }

        /// <summary>
        /// Mod's name.
        /// </summary>
        public string Name { get; set; }

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
        public ModState State {get; set;}

        public HashSet<ModSource> Sources { get; private set; }

        [JsonConstructor]
        public Mod(int id, HashSet<ModSource> sources = null)
        {
            ID = id;
            Sources = (sources == null ? new HashSet<ModSource>() : sources);
            Name = "";
            Version = "";
            Root = "";
        }
        public Mod() : this(Math.Abs(Guid.NewGuid().ToString().GetHashCode())){}

        public override bool Equals(object other)
        {
            if (other == null || !typeof(Mod).Equals(other.GetType())) return false;
            return ((Mod)other).ID == ID;
        }

        public override int GetHashCode() { return ID; }
    }
}
