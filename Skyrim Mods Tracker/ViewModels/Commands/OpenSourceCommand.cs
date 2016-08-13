using SMT.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SMT.ViewModels.Commands
{
    class OpenSourceCommand : BaseCommand<Source>
    {
        public OpenSourceCommand(Source target = null) : base(target){}
        public override bool CanExecute(Source parameter)
        {
            return parameter != null && parameter.HasValidURL;
        }

        public override void Execute(Source parameter)
        {
            if (!CanExecute(parameter)) return;
                Process.Start(parameter.URL);
        }
    }
}
