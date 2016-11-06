using SMT.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.ViewModels.Commands
{
    class SaveChangesCommand : BaseCommand<bool>
    {
        public override bool CanExecute(bool parameter)
        {
            return true;
        }

        public override void Execute(bool parameter)
        {
            ModsManager.NormalizeMods();
            StorageManager.Sync();
        }
    }
}
