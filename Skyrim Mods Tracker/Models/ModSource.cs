using Newtonsoft.Json;
using SMT.JsonConverters;
using SMT.Managers;
using SMT.Utils;
using System;

namespace SMT.Models
{
    class ModSource : SMTModel, IValidatable, IStateful<SourceState>, IVersioning, IRemote
    {
        private Version version;

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
        public string Version { get { return version.Value; } set { version.Value = value; } }

        /// <summary>
        /// Absolute url to the source's web-page.
        /// </summary>
        [JsonIgnore]
        public string URL
        {
            get { return (HasValidURL && HasKnownServer ? ModsManager.BuildModSourceURL(this) : Path); }
            set
            {
                Uri uri = null;
                HasValidURL = !string.IsNullOrWhiteSpace(value) && ModsManager.TryBuildModSourceURL(value, out uri); ;
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

        public SourceState State { get; set; }

        [JsonIgnore]
        public string StateString { get { return State.GetDescription(); } }

        public ModSource() : base(){}
        protected ModSource(int id) : base(id) { }

        protected override void Init()
        {
            Path = "";
            Server = null;
            Language = Language.None;
            version = new Version();
            HasValidURL = false; 
        }

        public override void Normalize()
        {
            Path = Path.Trim();
        }

        [JsonConstructor]
        private ModSource(int id, string path, Server server, Language lang, string version) : base(id)
        {
            URL = ModsManager.BuildModSourceURL(server, path);
            Language = lang;
            Version = version;
            Normalize();
        }
    }
}
