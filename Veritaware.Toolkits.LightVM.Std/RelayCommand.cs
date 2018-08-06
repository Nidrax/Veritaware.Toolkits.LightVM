using System;
using System.Windows.Input;

namespace Veritaware.Toolkits.LightVM.Std
{
    #region Non-generic RelayCommand

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
            => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public bool CanExecute(object parameter)
            => _canExecute is null || _canExecute();

        public virtual void Execute(object parameter) => Execute();

        public void Execute() => _execute?.Invoke();
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

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
            => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return _execute != null;

            var param = (T)parameter;
            return _canExecute(param);
        }

        public virtual void Execute(object parameter)
        {
            if (!(parameter is T param))
                throw new ArgumentException($"Expected parameter of type {typeof(T)}.");

            Execute(param);
        }


        public void Execute(T parameter) => _execute?.Invoke(parameter);
    }

    #endregion
}
