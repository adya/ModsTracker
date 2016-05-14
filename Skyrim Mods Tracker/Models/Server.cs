using Newtonsoft.Json;
using System;
using SMT.Managers;
using SMT.Models.PropertyInterfaces;
using System.Collections.Generic;

namespace SMT.Models
{
    class Server : SMTNamedModel<Server>, IRemote, IValidatable, ILocalizable, IStateful<ServerState>
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
        /// Represents primary language of the server.
        /// </summary>
        public Language Language { get; set; }

        public ServerState State { get; set; }

        /// <summary>
        /// Flag indicating whether the server has a valid url or not.
        /// </summary>
        [JsonIgnore]
        public bool HasValidURL { get; private set; }

        /// <summary>
        /// Flag indicating whether the server has a valid pattern or not.
        /// </summary>
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

        public override void CopyTo(Server server)
        {
            base.CopyTo(server);
            if (server == null) return;
            server.VersionPattern = this.VersionPattern;
            server.URL = this.URL;
            server.Language = this.Language;
            server.State = this.State;
            server.Cookies = this.Cookies;
        }
    }
}
