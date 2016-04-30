using Newtonsoft.Json;
using SMT.Utils;

namespace SMT.Models
{
    abstract class SMTNamedModel : SMTModel, INamed
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
    }
}
