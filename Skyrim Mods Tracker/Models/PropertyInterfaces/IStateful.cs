using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.Models
{
    interface IStateful<T> where T : struct, IConvertible
    {
        T State { get; set; }
    }
}
