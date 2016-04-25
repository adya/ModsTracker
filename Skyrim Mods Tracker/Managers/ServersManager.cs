using SMT.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace SMT.Managers
{
    class ServersManager
    {
        public static HashSet<Server> Servers { get { return StorageManager.Get<Server>(); } }

        public static Server ServerWithID(int id) { return Servers.FirstOrDefault(s => s.ID == id); }
        public static Server ServerWithURL(string url) { return Servers.FirstOrDefault(s => s.URL.Equals(url)); }
    }
}
