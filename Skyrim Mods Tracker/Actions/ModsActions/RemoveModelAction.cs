using SMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.Actions
{
    class RemoveModelAction<T> : NamedAction
    {
        private T model;
        private ICollection<T> target;

        public RemoveModelAction(ICollection<T> target, T model) : base("Remove mod") {
            IsValid = model != null && target != null && target.Contains(model);
            if (!IsValid) return;
            this.model = model;
            this.target = target;
        }
        public override void Perform()
        {
            if (!IsValid) return;
            target.Remove(model);
        }

        public override void Revert()
        {
            if (!IsValid) return;
            target.Add(model);
        }
    }
}
