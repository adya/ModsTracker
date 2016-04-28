using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.Models
{
    abstract class SMTNamedModel : SMTModel, INamed
    {
        /// <summary>
        /// Modle's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Checks whether the mod a has valid name or not.
        /// </summary>
        [JsonIgnore]
        public virtual bool HasValidName { get { return !string.IsNullOrWhiteSpace(Name); } }

        protected SMTNamedModel(int id) : base(id) {}
        protected SMTNamedModel() : base() {}

        protected override void Init() { Name = ""; }

        public override string ToString() { return Name; }
    }
}
