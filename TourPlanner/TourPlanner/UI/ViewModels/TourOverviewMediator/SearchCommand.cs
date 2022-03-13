using System;
using System.Windows.Input;

namespace TourPlanner.UI.ViewModels
{
    public class SearchCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public SearchCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }


        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;


        public void Execute(object parameter) => _execute.Invoke(parameter);


        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
