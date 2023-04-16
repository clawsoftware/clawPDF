using System;
using System.ComponentModel;
using System.Windows.Input;
using clawSoft.clawPDF.Shared.Properties;

namespace clawSoft.clawPDF.Shared.ViewModels
{
    /// <summary>
    ///     DelegateCommand implements the <see cref="System.Windows.Input.ICommand" /> interface and uses delegates for the
    ///     actual work (checking CanExecute and doing Execute)
    /// </summary>
    public class DelegateCommand : ICommand, INotifyPropertyChanged
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        /// <summary>
        ///     Create a new DelegateCommand with the given delegates. If canExecute is not provided, it will always return true.
        /// </summary>
        /// <param name="execute">Will be called when the command is executed</param>
        /// <param name="canExecute">
        ///     Will be executed to check if the Command can be executed. If no delegate is provided, this
        ///     will always return true
        /// </param>
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool IsExecutable => CanExecute(null);

        /// <summary>
        ///     CanExecuteChanged fires when the state of CanExecute (might) have changed
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Determines whether the command can be executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null) return true;

            return _canExecute(parameter);
        }

        /// <summary>
        ///     Execute the command
        /// </summary>
        /// <param name="parameter">Optional parameter for the command</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Raise The CanExecuteChanged event
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null) CanExecuteChanged(this, EventArgs.Empty);

            OnPropertyChanged("IsExecutable");
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}