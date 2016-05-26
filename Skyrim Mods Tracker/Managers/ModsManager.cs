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

        public static void UpdateStates() { foreach (var mod in Mods) mod.UpdateState(); }

        public static void NormalizeMods() { foreach (var mod in Mods) mod.Normalize(); }

        public static Mod ModWithID(int id) { return Mods.FirstOrDefault(m => m.ID == id); }

        #region Mod Extensions
       
        public static bool HasUniqueName(this Mod mod) { return (Mods.Count(m => !m.Equals(mod) && m.Name.Equals(mod.Name)) == 0); }
        #endregion
    }
}
