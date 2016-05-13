using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.Models.PropertyInterfaces
{
    /// <summary>
    /// Entity with language.
    /// </summary>
    interface ILocalizable
    {
        Language Language { get; set; }
    }
}
