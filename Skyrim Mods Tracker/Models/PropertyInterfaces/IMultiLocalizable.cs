using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.Models.PropertyInterfaces
{
    /// <summary>
    /// Entity with set of languages<para/>
    /// Note: Currently not used.
    /// </summary>
    interface IMultiLocalizable : ILocalizable
    {
        HashSet<Language> Languages { get; set; }
    }
}
