using SMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.Actions
{
    class AddModelAction<T> : NamedAction
    {
        private T model;
        private ICollection<T> target;

        public AddModelAction(ICollection<T> target, T model = default(T)) : base("Add model")
        {
            IsValid = target != null && model != null && !target.Contains(this.model);
            if (!IsValid) return;
            this.model = model;
            this.target = target;
        }

        public override void Perform()
        {
            if (!IsValid) return;
            target.Add(model);
        }

        public override void Revert()
        {
            if (!IsValid) return;
            target.Remove(model);
        }
    }
}
