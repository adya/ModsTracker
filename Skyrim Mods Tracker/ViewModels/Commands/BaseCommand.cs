using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SMT.ViewModels.Commands
{
    abstract class BaseCommand<T> : ICommand, INotifyPropertyChanged
    {
        public delegate void CommandExecutedHandler(BaseCommand<T> command, T parameter);
        public event PropertyChangedEventHandler PropertyChanged;

        public event CommandExecutedHandler CommandExecuted;
        public event EventHandler CanExecuteChanged;

        private T parameter;

        public T Parameter { get { return parameter; } set { parameter = value; OnPropertyChanged(); Update(); } }

        public BaseCommand(T param = default(T))
        {
            Parameter = parameter = param;
        }

        public abstract bool CanExecute(T parameter);
        public abstract void Execute(T parameter);

        public bool CanExecute(object parameter)
        {
            return (((parameter is T) && CanExecute((T)parameter)) ||
                    (CanExecute(Parameter)));
        }

        public void Execute(object parameter)
        {
            T actualParam = (parameter is T ? ((T)parameter) : Parameter);
            Execute(actualParam);
            if (CommandExecuted != null)
                CommandExecuted(this, actualParam);
        }

        public void Update()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
