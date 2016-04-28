using SMT.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace SMT.Managers
{
    static class ServersManager
    {
        public static HashSet<Server> Servers { get { return StorageManager.Get<Server>(); } }

        public static void FormatServers() { foreach (var server in Servers) server.Format(); }


        public static Server ServerWithID(int id) { return Servers.FirstOrDefault(s => s.ID == id); }
        public static Server ServerWithURL(string url) { return Servers.FirstOrDefault(s => s.URL.Equals(url)); }
        public static Server ServerWithURL(Uri uri) { return ServerWithURL(BuildServerURL(uri)); }


        public static string BuildServerURL(Uri uri)
        {
            if (uri == null) return "";
            return (uri.Scheme + "://" + uri.Host);
        }

        public static bool TryBuildServerURL(string url, out string serverUrl)
        {
            Uri uri;
            bool success = (Uri.TryCreate(url, UriKind.Absolute, out uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps));
            serverUrl = (success ? BuildServerURL(uri) : url);
            return success;
        }

        #region Server Extensions

        public static void Format(this Server server)
        {
            server.Name = server.Name.Trim();
            server.URL = server.URL.Trim();
            server.VersionPattern = server.VersionPattern.Trim();
        }

        public static bool IsAvailable(this Server server)
        {
            if (!server.HasValidURL) return false;
            WebRequest request = WebRequest.Create(server.URL);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return (response != null || response.StatusCode == HttpStatusCode.OK); 
        }

        public static bool HasUniqueName(this Server server) { return (Servers.Count(s => !s.Equals(server) && s.Name.Equals(server.Name)) == 0); }
        #endregion
    }
}
