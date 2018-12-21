using System;
using System.Windows.Input;

namespace ContactListTP.Configuration
{
    public class Command<T> : ICommand where T : class
    {
        private readonly Predicate<T> canExecute;
        private readonly Action<T> execute;

        public Command(Action<T> execute, Predicate<T> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute?.Invoke((T) parameter) ?? true;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public void Execute(object parameter)
        {
            execute((T) parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}