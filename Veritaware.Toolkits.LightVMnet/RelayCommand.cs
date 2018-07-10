using System;
using System.Windows.Input;

namespace Veritaware.Toolkits.LightVMnet
{
    #region Parameterless RelayCommand

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
            => _canExecute is null || _canExecute();

        public void Execute(object parameter)
            => _execute?.Invoke();
    }

    #endregion


    #region Generic RelayCommand

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return _execute != null;

            var param = (T)parameter;
            return _canExecute(param);
        }

        public void Execute(object parameter)
            => _execute?.Invoke((T)parameter);
    }

    #endregion
}
