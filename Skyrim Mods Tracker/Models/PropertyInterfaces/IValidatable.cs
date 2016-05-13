using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.Models.PropertyInterfaces
{
    /// <summary>
    /// Entity which can be validated.
    /// </summary>
    interface IValidatable
    {
        bool IsValid { get; }
    }
}
