using System;
using System.Windows.Input;

namespace TourPlanner.UI.ViewModels
{
    public class SearchCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public SearchCommand(Action<object> exe, Predicate<object> canExe)
        {
            _execute = exe ?? throw new ArgumentNullException(nameof(exe));
            _canExecute = canExe;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            _canExecute?.Invoke(parameter);
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
            throw new NotImplementedException();
        }

        public event EventHandler CommandChange
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
    }
}
