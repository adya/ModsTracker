using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.Models.PropertyInterfaces
{
    /// <summary>
    /// Entity with set of possible states. State determined by speecified enum.
    /// </summary>
    /// <typeparam name="T">Type of enum used to describe states.</typeparam>
    interface IStateful<T> where T : struct, IConvertible
    {
        T State { get; set; }
    }
}
