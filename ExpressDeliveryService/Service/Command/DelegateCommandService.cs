using System;
using System.Windows.Input;

namespace ExpressDeliveryService.Service.Command
{
    class DelegateCommandService : ICommand
    {
        Action<object> execute;

        Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
            {
                return canExecute(parameter);
            }
            else
            {
                return true;
            }
        }

        public void Execute(object parameter)
        {
            if (execute != null)
            {
                execute(parameter);
            }
        }

        public DelegateCommandService(Action<object> executeAction) : this(executeAction, null)
        {
            execute = executeAction;
        }

        public DelegateCommandService(Action<object> executeAction, Func<object, bool> canExecuteFunc)
        {
            canExecute = canExecuteFunc;
            execute = executeAction;
        }
    }
}
