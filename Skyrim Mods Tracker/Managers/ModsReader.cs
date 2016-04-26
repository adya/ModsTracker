using SMT.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SMT.Managers
{
    static class ModsReader
    {
        public static void CheckMod(Mod mod)
        {
            if (mod.Sources == null || mod.Sources.Count == 0) return;

            using (WebClient client = new WebClient())
            {
                client.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.112 Safari/537.36";
                client.Encoding = Encoding.UTF8;
                foreach (var src in mod.Sources)
                {
                    if (src.Server == null) {
                        src.State = ModSource.SourceState.UnknownServer;
                        continue;
                    }
                    string pattern = src.Server.VersionPattern;
                    try {
                        string htmlCode = client.DownloadString(src.URL);
                        Match m = Regex.Match(htmlCode, pattern);
                        if (m.Success)
                        {
                            src.Version = m.Groups[1].Value;
                            src.State = ModSource.SourceState.Available;
                            if (mod.State != Mod.ModState.Outdated && mod.Version.Equals(src.Version))
                            {
                                mod.State = Mod.ModState.UpToDate;
                            }
                            else
                                mod.State = Mod.ModState.Outdated;
                        }
                        else
                            src.State = ModSource.SourceState.UnavailableVersion;
                    }
                    catch(Exception e)
                    {
                        src.State = ModSource.SourceState.UnreachablePage;
                    }
                    
                }
            }
        }
    }
}
