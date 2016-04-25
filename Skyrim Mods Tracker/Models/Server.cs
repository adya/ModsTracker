using Newtonsoft.Json;
using System;

namespace SMT.Models
{
    class Server
    {

        /// <summary>
        /// Gets server id.
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Gets displayed name of the server.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets regular expression pattern which matches version label on mod's web-page.
        /// </summary>
        [JsonProperty("Pattern")]
        public string VersionPattern { get; set; }

        /// <summary>
        /// Gets server domain.
        /// </summary>
        public string URL { get; set; }

        [JsonConstructor]
        public Server(int id) { ID = id; Name = ""; VersionPattern = ""; URL = ""; }

        public Server() : this(Math.Abs(Guid.NewGuid().ToString().GetHashCode())) { }

        public override bool Equals(object other)
        {
            if (other == null || !typeof(Server).Equals(other.GetType())) return false;
            return ((Server)other).ID == this.ID;
        }

        public override int GetHashCode() { return ID; }

        public override string ToString()
        {
            return Name;
        }
    }
}
