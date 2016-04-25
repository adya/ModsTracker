using SMT.Models;
using System;
using System.Collections.Generic;

namespace SMT.Managers
{
    class ModsManager
    {
        public static HashSet<Mod> Mods { get { return StorageManager.Get<Mod>(); } }
        public static void VerifyAll()
        {
            foreach (var mod in Mods)
            {
                if (mod.Sources == null || mod.Sources.Count == 0) mod.State = Mod.ModState.NotTracking;
                else if (mod.Root == null || mod.Root == "") mod.State = Mod.ModState.MissedFile;
            }
        }
    }
}
