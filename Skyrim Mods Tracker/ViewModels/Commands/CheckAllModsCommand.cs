using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.ViewModels.Commands
{
    class CheckAllModsCommand : BaseCommand<MainViewModel>
    {
        public CheckAllModsCommand(MainViewModel target) : base(target) { }

        public override bool CanExecute(MainViewModel parameter)
        {
            return parameter.Mods.Count > 0;
        }

        public override void Execute(MainViewModel parameter)
        {
            parameter.RunCheckMods(parameter.Mods.ToArray());
        }
    }
}
