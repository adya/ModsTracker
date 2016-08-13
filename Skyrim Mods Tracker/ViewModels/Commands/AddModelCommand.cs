using SMT.Actions;
using SMT.Managers;
using SMT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.ViewModels.Commands
{
    class AddModelCommand<T> : BaseCommand<T>
    {
        private ActionsManager manager;
        private ObservableCollection<T> collection;

        public AddModelCommand(ObservableCollection<T> collection, ActionsManager manager = null) : this(collection, default(T), manager) { }

        public AddModelCommand(ObservableCollection<T> collection, T model = default(T), ActionsManager manager = null) : base(model)
        {
            this.collection = collection;
            this.manager = manager;
        }
        public override bool CanExecute(T parameter)
        {
            return collection != null && parameter != null;
        }

        public override void Execute(T parameter)
        {
            if (!CanExecute(parameter)) return;
            IAction action = new AddModelAction<T>(collection, parameter);
            if (manager != null)
                manager.PerformAction(action);
            else
                action.Perform();
        }
    }
}
