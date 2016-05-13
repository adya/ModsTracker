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
        public static void CheckUpdates() { foreach (var mod in Mods) mod.CheckUpdate();  }
        public static Mod ParseMod(string str)
        {
            Match m = Regex.Match(str, SettingsManager.NamePattern);

            var nameG = m.Groups[SettingsManager.PATTERN_NAME_GROUP];
            var versionG = m.Groups[SettingsManager.PATTERN_VERSION_GROUP];

            if (nameG == null) return null;

            Mod mod = new Mod();
            mod.Name = nameG.Value.Trim();
            if (versionG != null) mod.Version = new Models.Version(versionG.Value.Trim());

            return mod;   
        }

        public static void UpdateStates() { foreach (var mod in Mods) mod.UpdateState(); }

        public static void NormalizeMods() { foreach (var mod in Mods) mod.Normalize(); }

        public static Mod ModWithID(int id) { return Mods.FirstOrDefault(m => m.ID == id); }
        public static Mod ModWithRoot(string root) { return Mods.FirstOrDefault(m => m.HasValidFileName && m.FileName.Equals(root)); }

        #region Mod Extensions
       
        public static string BuildPatternFilename(this Mod mod)
        {
            return string.Format("{0}{1}{2}{3}", 
                                mod.Name.Trim(),
                                (string.IsNullOrWhiteSpace(mod.Version.Value) ? "" : " (" + mod.Version.Value + ")"),
                                (mod.Sources.Count > 0 ? " (" + string.Join(", ", mod.Sources.Select(s => s.Language.ToShortString()).Distinct().OrderByDescending(s => s)) + ")" : ""),
                                Path.GetExtension(mod.FileName));
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
        #endregion
    }
}
