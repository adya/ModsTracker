using SMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.ViewModels.Commands
{
    class CheckSourceStateCommand : BaseCommand<SourceItemViewModel>
    {
        private MainViewModel Parent { get; set; }
        private Mod Owner { get; set; }

        public CheckSourceStateCommand(SourceItemViewModel target, Mod owner, MainViewModel parent) : base(target)
        {
            Parent = parent;
            Owner = owner;
        }

        public override bool CanExecute(SourceItemViewModel parameter)
        {
            return parameter.Source.HasValidURL;
        }

        public override void Execute(SourceItemViewModel parameter)
        {
            Parent.RunCheckSources(Owner, parameter);
        }
    }
}
