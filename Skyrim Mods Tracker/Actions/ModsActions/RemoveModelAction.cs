using SMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.Actions
{
    class RemoveModelAction<T> : NamedAction where T : SMTModel<T>, new()
    {
        private T model;
        private ICollection<T> target;

        public RemoveModelAction(T model, ICollection<T> target) : base("Remove mod") {
            IsValid = model != null && target != null && target.Contains(this.model);
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
