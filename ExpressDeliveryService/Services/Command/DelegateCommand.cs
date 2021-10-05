using System;
using System.Windows.Input;

namespace ExpressDeliveryService.Services.Command
{
    public sealed class DelegateCommandService : ICommand
    {
        Action<object> _execute;

        Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
            {
                return _canExecute(parameter);
            }
            return true;
        }

        public void Execute(object parameter)
        {
            if (_execute != null)
            {
                _execute(parameter);
            }
        }

        public DelegateCommandService(Action<object> executeAction) : this(executeAction, null)
        {
            _execute = executeAction;
        }

        public DelegateCommandService(Action<object> executeAction, Func<object, bool> canExecuteFunc)
        {
            _canExecute = canExecuteFunc;
            _execute = executeAction;
        }
    }
}
