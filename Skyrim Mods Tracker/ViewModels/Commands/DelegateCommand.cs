using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SMT.ViewModels.Commands
{
    class DelegateCommand<T> : BaseCommand<T>
    {
        public Action<T> MethodToExecute { get; private set; }
        public Predicate<T> CanExecutePredicate { get; private set; }

        public DelegateCommand(Action<T> methodToExecute, Predicate<T> canExecutePredicate)
        {
            MethodToExecute = methodToExecute;
            CanExecutePredicate = canExecutePredicate;
        }
        public DelegateCommand(Action<T> methodToExecute) : this(methodToExecute, null) { }
        public override bool CanExecute(T parameter)
        {
            return CanExecutePredicate == null || CanExecutePredicate.Invoke(parameter);
        }
        public override void Execute(T parameter)
        {
            if (CanExecute(parameter))
                MethodToExecute.Invoke(parameter);
        }
    }
}
