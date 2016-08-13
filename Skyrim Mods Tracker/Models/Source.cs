using Newtonsoft.Json;
using SMT.JsonConverters;
using SMT.Managers;
using SMT.Models.PropertyInterfaces;
using System;

namespace SMT.Models
{
    class Source : SMTModel<Source>, IValidatable, IStateful<SourceState>, IVersioning, IRemote, ILocalizable
    {
        private string path;
        private Server server;
        private Language language;
        private Version version;
        private SourceState state;

        /// <summary>
        /// Relative url to the source's web-page.
        /// </summary>
        public string Path { get { return path; } private set { path = value; OnPropertyChanged(); } }

        /// <summary>
        /// Domain server.
        /// </summary>
        [JsonConverter(typeof(ServerIDJsonConverter))]
        public Server Server { get { return server; } private set { server = value; OnPropertyChanged(); } }

        /// <summary>
        /// Language of the mod available at this source.
        /// </summary>
        public Language Language { get { return language; } set { language = value; OnPropertyChanged(); } }

        /// <summary>
        /// Latest version available at this source.
        /// </summary>
        public Version Version
        {
            get { return version; }
            set
            {
                if (version != null)
                    version.PropertyChanged -= Version_PropertyChanged;
                version = value;
                version.PropertyChanged += Version_PropertyChanged;
                OnPropertyChanged();
            }
        }

        private void Version_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged("Version");
        }


        /// <summary>
        /// State of the source.
        /// </summary>
        public SourceState State { get { return state; } set { state = value; OnPropertyChanged(); } }

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
        public bool HasValidVersion { get { return Version != null && Version.Value != ""; } }

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
            else if (!HasValidVersion)
                State = SourceState.UnavailableVersion;
        }

        public override void CopyTo(Source source)
        {
            if (source == null) return;
            source.Path = this.Path;
            source.Server = this.Server;
            source.Language = this.Language;
            source.Version = this.Version;
            source.HasValidURL = this.HasValidURL;
        }
    }
}
