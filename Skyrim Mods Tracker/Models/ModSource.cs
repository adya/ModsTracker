using Newtonsoft.Json;
using SMT.JsonConverters;
using SMT.Managers;
using SMT.Utils;
using System;

namespace SMT.Models
{
    class ModSource : SMTModel, IValidatable, IStateful<SourceState>, IVersioning, IRemote
    {
        private string version;

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
        public string Version { get { return version; } set { version = StringUtils.NonNull(value); } }

        /// <summary>
        /// Absolute url to the source's web-page.
        /// </summary>
        [JsonIgnore]
        public string URL
        {
            get { return (HasValidURL ? ModsManager.BuildModSourceURL(this) : ""); }
            set
            {
                Uri uri = null;
                HasValidURL = !string.IsNullOrWhiteSpace(value) && ModsManager.TryBuildModSourceURL(value, out uri);;
                if (HasValidURL)
                {
                    Server = ServersManager.ServerWithURL(uri);
                    HasValidURL = (Server != null);
                    Path = uri.PathAndQuery;
                }
                else
                { 
                    Path = "";
                    Server = null;
                }
            }
        }

        /// <summary>
        /// Flag indicating whether the source has a valid url or not. <para/>
        /// Note: This includes validating server using the list of known servers.
        /// </summary>
        [JsonIgnore]
        public bool HasValidURL { get; protected set; }

        /// <summary>
        /// Checks whether the source a has valid version or not.
        /// </summary>
        [JsonIgnore]
        public bool HasValidVersion { get { return !string.IsNullOrWhiteSpace(Version); } }

        [JsonIgnore]
        public bool IsValid { get { return HasValidURL && HasValidVersion; }}

        [JsonIgnore]
        public SourceState State { get; set; }

        public ModSource() : base(){}
        protected ModSource(int id) : base(id) { }

        protected override void Init()
        {
            Path = "";
            Server = null;
            Language = Language.None;
            Version = "";
            HasValidURL = false; 
        }

        public override void Normalize()
        {
            Path = Path.Trim();
            Version = Version.Trim();
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
