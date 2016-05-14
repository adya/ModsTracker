using SMT.Models.PropertyInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.Actions
{
    public interface IAction
    {
        string Name { get; }
        bool IsRevertable { get; }
        bool IsValid { get; }

        void Perform();
        void Revert();
    }
}
