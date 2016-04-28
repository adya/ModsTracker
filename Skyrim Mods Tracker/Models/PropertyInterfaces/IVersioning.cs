using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.Models
{
    interface IVersioning
    {
        string Version { get; set; }
        bool HasValidVersion { get; }
    }
}
