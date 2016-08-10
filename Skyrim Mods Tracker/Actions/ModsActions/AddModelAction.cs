using SMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.Actions
{
    class AddModelAction<T> : NamedAction where T : SMTModel<T>, new()
    {
        private T model;
        private ICollection<T> target;

        public AddModelAction(ICollection<T> target, T model = default(T)) : base("Add model")
        {
            IsValid = target != null && !target.Contains(this.model);
            if (!IsValid) return;
            this.model = model;
            this.target = target;
        }

        public override void Perform()
        {
            if (!IsValid) return;
            if (model != default(T))
                model = new T();
            target.Add(model);
        }

        public override void Revert()
        {
            if (!IsValid) return;
            target.Remove(model);
        }
    }
}
