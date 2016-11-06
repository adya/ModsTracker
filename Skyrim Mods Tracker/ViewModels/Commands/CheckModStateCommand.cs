using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.ViewModels.Commands
{
    class CheckModStateCommand : BaseCommand<ModItemViewModel>
    {
        private MainViewModel MainViewModel { get; set; }

        public CheckModStateCommand(ModItemViewModel target, MainViewModel mainViewModel) : base(target) {
            MainViewModel = mainViewModel;
        }

        public override bool CanExecute(ModItemViewModel parameter)
        {
            return parameter.Mod.Sources.Count > 0;
        }

        public override void Execute(ModItemViewModel parameter)
        {
            MainViewModel.RunCheckMods(parameter);
        }
    }
}
