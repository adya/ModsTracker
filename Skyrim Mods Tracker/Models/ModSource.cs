﻿using Newtonsoft.Json;
using SMT.JsonConverters;
using SMT.Managers;
using System;

namespace SMT.Models
{
    class ModSource
    {

        public enum SourceState { Undefined, UnknownServer, UnreachablePage, UnavailableVersion, Available}

        /// <summary>
        /// Relative path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Domain server.
        /// </summary>
        [JsonConverter(typeof(ServerIDJsonConverter))]
        public Server Server { get; set; }

        /// <summary>
        /// Language of the mod available at this source.
        /// </summary>
        public Language Language { get; set; }

        /// <summary>
        /// Latest version available at this source.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// State of that source.
        /// </summary>
        public SourceState State { get; set; }

        [JsonIgnore]
        public string URL
        {
            get { return (Server != null ? Server.URL + Path : ""); }
            set
            {
                Uri uri;
                bool valid = Uri.TryCreate(value, UriKind.Absolute, out uri);
                if (valid)
                {
                    Server = ServersManager.ServerWithURL(ServersManager.BuildURL(uri));
                    Path = uri.PathAndQuery;
                }
                else
                { 
                    Path = "";
                    Server = null;
                }
            }
        }

        public ModSource()
        {
            Path = "";
            Version = "";
        }

        public override bool Equals(object other)
        {
            if (Server == null || Path == null) return false;
            if (other == null || !typeof(ModSource).Equals(other.GetType())) return false;
            ModSource o = (ModSource)other;
            if (o.Server == null || o.Path == null) return false;
            return o.Server.Equals(this.Server) && o.Path.Equals(this.Path);
        }

        public override int GetHashCode()
        {
            if (Server == null) return 0;
            return Path.GetHashCode() * Server.GetHashCode() << Server.GetHashCode();
        }

    }
}
