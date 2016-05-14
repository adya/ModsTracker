using SMT.Models;
using SMT.Models.PropertyInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.Actions.ModsActions
{
    class EditSMTModelAction<T> : NamedAction where T : SMTModel<T>{ 

        private T target;
        private T oldModel;
        private T newModel;

        public EditSMTModelAction(T target, T newModel) : base("Edit model")
        {
            IsValid = target != null && newModel != null && !target.Equals(newModel);
            if (!IsValid) return;
            this.target = target;
            oldModel = target.Clone();
            newModel = newModel.Clone();
        }

        public override void Perform()
        {
            if (!IsValid) return;
            newModel.CopyTo(target);
        }

        public override void Revert()
        {
            if (!IsValid) return;
            oldModel.CopyTo(target);
        }
    }
}
