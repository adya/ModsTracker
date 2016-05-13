using SMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SMT.Managers
{
    static class SourcesManager
    {
        public static HashSet<Source> Sources { get { return StorageManager.Get<Source>(); } }

        public static string BuildModSourceURL(Source source) { return BuildModSourceURL(source.Server, source.Path); }

        public static string BuildModSourceURL(Server server, string relativePath) { return (server != null && !string.IsNullOrWhiteSpace(relativePath) ? server.URL + relativePath : ""); }

        public static bool TryBuildModSourceURL(string url, out Uri sourceUri)
        {
            bool success = (Uri.TryCreate(url, UriKind.Absolute, out sourceUri) &&
                            (sourceUri.Scheme == Uri.UriSchemeHttp || sourceUri.Scheme == Uri.UriSchemeHttps) &&
                            sourceUri.Segments.Length > 1);
            return success;
        }

        public static bool TryBuildModSource(string url, out Source src)
        {
            Uri uri;
            if (TryBuildModSourceURL(url, out uri))
            {
                src = new Source();
                src.URL = url;
            }
            else
                src = null;

            return src != null;

        }

        public static void UpdateStates() { foreach (var src in Sources) src.UpdateState(); }

        #region Source Extensions

        public static void CheckUpdate(this Source src)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.112 Safari/537.36";
                client.Encoding = Encoding.UTF8;

                if (src.Server == null)
                    src.State = SourceState.UnknownServer;
                else if (!src.Server.HasValidPattern)
                    src.State = SourceState.BrokenServer;
                else
                {
                    client.Headers[HttpRequestHeader.Cookie] = src.Server.Cookies;
                    string pattern = src.Server.VersionPattern;
                    string htmlCode = "";

                    try { htmlCode = client.DownloadString(src.URL); }
                    catch (Exception e) { src.State = SourceState.UnreachablePage; return; }

                    Match m = Regex.Match(htmlCode, pattern);
                    if (m.Success)
                    {
                        src.Version = new Models.Version(m.Groups[1].Value);
                        src.State = SourceState.Available;
                    }
                    else
                        src.State = SourceState.UnavailableVersion;
                }
            }
        }
        #endregion
    }
}
