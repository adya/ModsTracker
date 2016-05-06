using Newtonsoft.Json;
using System;
using SMT.Managers;

namespace SMT.Models
{
    class Server : SMTNamedModel, IRemote, IValidatable
    {
        [JsonIgnore]
        private string url;

        /// <summary>
        /// Regular expression pattern which matches version label on mod's web-page.
        /// </summary>
        [JsonProperty("Pattern")]
        public string VersionPattern { get; set; }

        /// <summary>
        /// Server's domain.
        /// </summary>
        public string URL
        {
            get { return url; }
            set { HasValidURL = ServersManager.TryBuildServerURL(value, out url); }
        }

        /// <summary>
        /// Flag indicating whether the server has a valid url or not.
        /// </summary>
        [JsonIgnore]
        public bool HasValidURL { get; private set; }

        [JsonIgnore]
        public bool HasValidPattern
        {
            get
            {
                if (string.IsNullOrWhiteSpace(VersionPattern)) return false;
                try { System.Text.RegularExpressions.Regex.IsMatch("", VersionPattern); return true; }
                catch (ArgumentException) { return false; }
            }
        }

        /// <summary>
        /// Checks whether the server has a valid configuration.
        /// </summary>
        [JsonIgnore]
        public bool IsValid { get { return HasValidName && HasValidURL && this.HasValidPattern; } }


        /// <summary>
        /// Cookies for server page in case server requires to be logged in in order tp access content.
        /// </summary>
        public string Cookies { get; set; }

        [JsonConstructor]
        protected Server(int id) : base(id) {}
        public Server() : base() {}

        protected override void Init()
        {
            base.Init();
            URL = "";
            VersionPattern = "";
        }
    }
}
