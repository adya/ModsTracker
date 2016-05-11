using SMT.Models;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using SMT.Utils;
using System;

namespace SMT.Managers
{
    static class ModsManager
    {
        public static HashSet<Mod> Mods { get { return StorageManager.Get<Mod>(); } }
        public static void CheckUpdates() { foreach (var mod in Mods) mod.CheckUpdates();  }
        public static Mod ParseMod(string str)
        {
            Match m = Regex.Match(str, SettingsManager.NamePattern);

            var nameG = m.Groups[SettingsManager.PATTERN_NAME_GROUP];
            var versionG = m.Groups[SettingsManager.PATTERN_VERSION_GROUP];

            if (nameG == null) return null;

            Mod mod = new Mod();
            mod.Name = nameG.Value.Trim();
            if (versionG != null) mod.Version = versionG.Value.Trim();

            return mod;   
        }

        public static void UpdateStates() { foreach (var mod in Mods) mod.UpdateState(); }

        private static Language ParseLanguage(string str)
        {
            Language l = Language.None;
            if (string.IsNullOrWhiteSpace(str)) return l;
            Enum.TryParse(str, out l);
            return l;
        }

        private static string ToShortString(this Language lang)
        {
            switch (lang)
            {
                default:
                case Language.None: return "NO";
                case Language.Russian: return "RU";
                case Language.English: return "EN";
            }
        }

        public static void NormalizeMods() { foreach (var mod in Mods) mod.Normalize(); }

        public static Mod ModWithID(int id) { return Mods.FirstOrDefault(m => m.ID == id); }
        public static Mod ModWithRoot(string root) { return Mods.FirstOrDefault(m => m.HasValidFileName && m.FileName.Equals(root)); }

        public static string BuildModSourceURL(ModSource source) { return BuildModSourceURL(source.Server, source.Path); }
       
        public static string BuildModSourceURL(Server server, string relativePath) { return (server != null && !string.IsNullOrWhiteSpace(relativePath) ? server.URL + relativePath : ""); }

        public static bool TryBuildModSourceURL(string url, out Uri sourceUri)
        {
            bool success = (Uri.TryCreate(url, UriKind.Absolute, out sourceUri) && 
                            (sourceUri.Scheme == Uri.UriSchemeHttp || sourceUri.Scheme == Uri.UriSchemeHttps) &&
                            sourceUri.Segments.Length > 1);
            return success;
        }

       

        public static bool TryBuildModSource(string url, out ModSource src)
        {
            Uri uri;
            if (TryBuildModSourceURL(url, out uri))
            {
                src = new ModSource();
                src.URL = url;
            }
            else
                src = null;

            return src != null;
            
        }

        public static void UpdateState(this ModSource src, Mod mod)
        {
            if (src.State == SourceState.UnreachablePage || 
                src.State == SourceState.UnavailableVersion) return; // there was an error while updating info, and therefore we can't update state

            if (!src.HasKnownServer)
                src.State = SourceState.UnknownServer;
            else if (!src.Server.HasValidPattern)
                src.State = SourceState.BrokenServer;
            else
            {
                switch (mod.Version.CompareTo(src.Version))
                {
                    default:
                    case 0: src.State = SourceState.Available; break;
                    case -1: src.State = SourceState.Update; break;
                    case 1: src.State = SourceState.Outdated; break;
                }
            }
        }   

        #region Mod Extensions

        public static void UpdateState(this Mod mod)
        {
            foreach (var src in mod.Sources) src.UpdateState(mod);
            if (string.IsNullOrWhiteSpace(mod.FileName))
                mod.State = ModState.MissedFile;
            else if (!mod.HasValidFileName)
                mod.State = ModState.InvlaidFilePath;
            else if (mod.Sources == null || mod.Sources.Count == 0)
                mod.State = ModState.NotTracking;
            else if (mod.Sources.Count(s => s.State == SourceState.Update) > 0)
                mod.State = ModState.Outdated;
            else if (mod.Sources.Count(s => s.State == SourceState.UnknownServer ||
                                            s.State == SourceState.BrokenServer ||
                                            s.State == SourceState.UnavailableVersion ||
                                            s.State == SourceState.UnreachablePage) > 0)
                mod.State = ModState.NotTracking;
            else
                mod.State = ModState.UpToDate;
        }

        public static void CheckUpdates(this Mod mod)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.112 Safari/537.36";
                client.Encoding = Encoding.UTF8;
                
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
                    client.Headers[HttpRequestHeader.Cookie] = src.Server.Cookies;
                    string pattern = src.Server.VersionPattern;
                    string htmlCode = "";

                    try { htmlCode = client.DownloadString(src.URL); }
                    catch (Exception e) { src.State = SourceState.UnreachablePage; continue; }

                    Match m = Regex.Match(htmlCode, pattern);
                    if (m.Success)
                    {
                        src.Version = m.Groups[1].Value;
                        switch (mod.Version.CompareTo(src.Version))
                        {
                            default:
                            case 0: src.State = SourceState.Available; break;
                            case -1: src.State = SourceState.Update; break;
                            case 1: src.State = SourceState.Outdated; break;
                        }
                    }
                    else
                        src.State = SourceState.UnavailableVersion;
                }
            }
            mod.UpdateState();
        }

        public static string BuildPatternFilename(this Mod mod)
        {
            string modStr = string.Format("{0}{1}{2}{3}", mod.Name.Trim(), (string.IsNullOrWhiteSpace(mod.Version) ? "" : " (" + mod.Version + ")"), (mod.Sources.Count > 0 ? " (" + string.Join(", ", mod.Sources.Select(s => s.Language.ToShortString()).Distinct().OrderByDescending(s => s)) + ")" : ""), Path.GetExtension(mod.FileName));
            return modStr;
        }
        public static void UpdateFilename(this Mod mod)
        {
            string newName = mod.BuildPatternFilename();
            if (SettingsManager.AutoRename && SettingsManager.HasValidModsLocation)
            {
                if (File.Exists(Path.Combine(SettingsManager.ModsLocation, mod.FileName)) && 
                    !File.Exists(Path.Combine(SettingsManager.ModsLocation, newName)))
                {
                    File.Move(Path.Combine(SettingsManager.ModsLocation, mod.FileName), Path.Combine(SettingsManager.ModsLocation, newName));
                    mod.FileName = newName;
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
