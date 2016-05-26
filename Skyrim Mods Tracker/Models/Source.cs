using Newtonsoft.Json;
using SMT.JsonConverters;
using SMT.Managers;
using SMT.Models.PropertyInterfaces;
using System;

namespace SMT.Models
{
    class Source : SMTNamedModel<Source>, IValidatable, IStateful<SourceState>, IVersioning, IRemote, ILocalizable
    {
        /// <summary>
        /// Relative url to the source's web-page.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Domain server.
        /// </summary>
        [JsonConverter(typeof(ServerIDJsonConverter))]
        public Server Server { get; private set; }

        /// <summary>
        /// Language of the mod available at this source.
        /// </summary>
        public Language Language { get; set; }

        /// <summary>
        /// Latest version available at this source.
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        /// State of the source.
        /// </summary>
        public SourceState State { get; set; }

        /// <summary>
        /// String representation of the sources's state.
        /// </summary>
        [JsonIgnore]
        public string StateString { get { return State.GetDescription(); } }

        /// <summary>
        /// Absolute url to the source's web-page.
        /// </summary>
        [JsonIgnore]
        public string URL
        {
            get { return (HasValidURL && HasKnownServer ? SourcesManager.BuildModSourceURL(this) : Path); }
            set
            {
                Uri uri = null;
                HasValidURL = !string.IsNullOrWhiteSpace(value) && SourcesManager.TryBuildModSourceURL(value, out uri); ;
                if (HasValidURL)
                {
                    Server = ServersManager.ServerWithURL(uri);
                    Path = ((Server != null) ? uri.PathAndQuery : value.Trim());
                }
            }
        }

        /// <summary>
        /// Flag indicating whether the source has a valid url or not. <para/>
        /// Note: This only validates url format.
        /// </summary>
        [JsonIgnore]
        public bool HasValidURL { get; protected set; }

        /// <summary>
        /// Flag indicating whether the source has a url domain of known server or not.
        /// </summary>
        [JsonIgnore]
        public bool HasKnownServer { get { return Server != null; } }

        /// <summary>
        /// Checks whether the source a has valid version or not.
        /// </summary>
        [JsonIgnore]
        public bool HasValidVersion { get { return Version != null; } }

        [JsonIgnore]
        public bool IsValid { get { return HasValidURL && HasKnownServer && HasValidVersion; }}


        public Source() : base(){}
        protected Source(int id) : base(id) { }

        [JsonConstructor]
        private Source(int id, string path, Server server, Language lang, string version) : base(id)
        {
            URL = SourcesManager.BuildModSourceURL(server, path);
            Language = lang;
            Version = new Version(version);
            Normalize();
        }

        protected override void Init()
        {
            base.Init();
            Path = "";
            Server = null;
            Language = Language.None;
            Version = new Version();
            HasValidURL = false; 
        }

        public override void Normalize()
        {
            Path = Path.Trim();
        }

        public void UpdateState()
        {
            if (State == SourceState.UnreachablePage ||
                State == SourceState.UnavailableVersion) return; // there was an error while updating info, and therefore we can't update state

            if (!HasKnownServer)
                State = SourceState.UnknownServer;
            else if (!Server.HasValidPattern)
                State = SourceState.BrokenServer;
        }

        public override void CopyTo(Source source)
        {
            base.CopyTo(source);
            if (source == null) return;
            source.Path = this.Path;
            source.Server = this.Server;
            source.Language = this.Language;
            source.Version = this.Version;
            source.HasValidURL = this.HasValidURL;
        }
    }
}
