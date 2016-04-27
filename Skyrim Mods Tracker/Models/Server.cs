using Newtonsoft.Json;
using System;
using SMT.Managers;

namespace SMT.Models
{
    class Server
    {
        [JsonIgnore]
        private string url;

        /// <summary>
        /// Server id.
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Name of the server.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Regular expression pattern which matches version label on mod's web-page.
        /// </summary>
        [JsonProperty("Pattern")]
        public string VersionPattern { get; set; }

        /// <summary>
        /// Flag indicating whether server a has valid url or not.
        /// </summary>
        [JsonIgnore]
        public bool HasValidURL { get; private set; }

        /// <summary>
        /// Flag indicating whether server a has valid name or not.
        /// </summary>
        [JsonIgnore]
        public bool HasValidName { get { return !string.IsNullOrWhiteSpace(Name); } }

        [JsonIgnore]
        public bool IsValid { get { return HasValidName && HasValidURL && this.HasValidPattern(); } }

        /// <summary>
        /// Server domain.
        /// </summary>
        public string URL
        {
            get { return url; }
            set { HasValidURL = ServersManager.TryBuildURL(value, out url); }
        }

        [JsonConstructor]
        public Server(int id) { ID = id; Name = ""; VersionPattern = ""; URL = ""; }

        public Server() : this(Math.Abs(Guid.NewGuid().ToString().GetHashCode())) { }

        public override bool Equals(object other)
        {
            if (other == null || !typeof(Server).Equals(other.GetType())) return false;
            return ((Server)other).ID == this.ID;
        }

        public override int GetHashCode() { return ID; }

        public override string ToString() { return Name; }
    }
}
