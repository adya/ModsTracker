using System;
using Newtonsoft.Json;
using SMT.Models.PropertyInterfaces;
using SMT.Utils;

namespace SMT.Models
{
    public abstract class SMTNamedModel<T> : SMTModel<T>, INamed where T : SMTNamedModel<T>, new()
    {
        private string name; // field for preventing null values.

        /// <summary>
        /// Modle's name.
        /// </summary>
        public string Name { get { return name; } set { name = StringUtils.NonNull(value); } }

        /// <summary>
        /// Checks whether the mod a has valid name or not.
        /// </summary>
        [JsonIgnore]
        public virtual bool HasValidName { get { return !string.IsNullOrWhiteSpace(Name); } }

        protected SMTNamedModel(int id) : base(id) {}
        protected SMTNamedModel() : base() {}

        protected override void Init() { Name = ""; }

        public override void Normalize()
        {
            Name = Name.Trim();
        }

        public override string ToString() { return Name; }

        public override T Clone()
        {
            T model = new T();
            this.CopyTo(model);
            return model;
        }

        public override void CopyTo(T model)
        {
            if (model == null) return;
            model.Name = this.Name;
        }
    }
}
