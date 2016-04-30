using SMT.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace SMT.Managers
{
    static class ModsManager
    {
        public static HashSet<Mod> Mods { get { return StorageManager.Get<Mod>(); } }
        public static void CheckUpdates() { foreach (var mod in Mods) CheckUpdates(mod);  }

        public static void NormalizeMods() { foreach (var mod in Mods) mod.Normalize(); }

        public static Mod ModWithID(int id) { return Mods.FirstOrDefault(m => m.ID == id); }
        public static Mod ModWithRoot(string root) { return Mods.FirstOrDefault(m => m.HasValidRoot && m.Root.Equals(root)); }

        public static string BuildModSourceURL(ModSource source) { return BuildModSourceURL(source.Server, source.Path); }
       
        public static string BuildModSourceURL(Server server, string relativePath) { return (server != null && !string.IsNullOrWhiteSpace(relativePath) ? server.URL + relativePath : ""); }

        public static bool TryBuildModSourceURL(string url, out Uri sourceUri)
        {
            bool success = (Uri.TryCreate(url, UriKind.Absolute, out sourceUri) && 
                            (sourceUri.Scheme == Uri.UriSchemeHttp || sourceUri.Scheme == Uri.UriSchemeHttps) &&
                            sourceUri.Segments.Length > 1);
            return success;
        }

        #region Mod Extensions

        public static void CheckUpdates(this Mod mod)
        {
            if (mod.Sources == null || mod.Sources.Count == 0) { mod.State = ModState.NotTracking; return; }

            if (!mod.HasValidRoot) mod.State = ModState.MissedFile;

            using (WebClient client = new WebClient())
            {
                client.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.112 Safari/537.36";
                client.Encoding = Encoding.UTF8;
                bool hasUpdateSource = false;
                foreach (var src in mod.Sources)
                {
                    if (src.Server == null)
                    {
                        src.State = SourceState.UnknownServer;
                        continue;
                    }
                    else if (!src.Server.HasValidPattern)
                    {
                        src.State = SourceState.BrokenServer;
                        continue;
                    }
                    string pattern = src.Server.VersionPattern;
                    string htmlCode = "";
                    try { htmlCode = client.DownloadString(src.URL); }
                    catch (Exception e) { src.State = SourceState.UnreachablePage; }

                    Match m = Regex.Match(htmlCode, pattern);
                        if (m.Success)
                        {
                            src.Version = m.Groups[1].Value;
                            if (mod.Version.Equals(src.Version))
                            {
                                if (!hasUpdateSource) mod.State = ModState.UpToDate; // ensures that mod will have outdated state if at least one source has update
                                src.State = SourceState.Available;
                            }
                            else
                            {
                                hasUpdateSource = true;
                                mod.State = ModState.Outdated;
                                src.State = SourceState.Update;
                            }
                        }
                        else src.State = SourceState.UnavailableVersion;
                }
            }
        }

        public static bool HasUniqueName(this Mod mod) { return (Mods.Count(m => !m.Equals(mod) && m.Name.Equals(mod.Name)) == 0); }

        //public static bool HasInvalidSources(this Mod mod)
        //{
        //    if (mod.Sources == null) return false;

        //    mod.Sources.Count(s => s.IsValid);
        //}
        #endregion
    }
}
