using SMT.Models.PropertyInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.Actions
{
    abstract class NamedAction : IAction
    {
        public string Name { get; private set; }
        public bool IsRevertable { get; protected set; }
        public bool IsValid { get; protected set; }

        protected NamedAction(string name) { Name = name; IsRevertable = true; IsValid = true; }

        public abstract void Perform();
        public abstract void Revert();
    }
}
