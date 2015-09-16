using System;
using System.Windows.Input;

namespace RolerFramework.Command
{
    public class RolerCommand : ICommand
    {
        #region Feilds

        private readonly Action _execute;

        private readonly Func<bool> _canExecute;

        #endregion

        #region Constructor

        public RolerCommand(Action execute)
            : this(execute, null)
        {
        }

        public RolerCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this._execute = execute;

            if (canExecute != null)
            {
                this._canExecute = canExecute;
            }
        }

        #endregion

        public event EventHandler CanExecuteChanged;

        public void onCanExecuteChanged()
        {
            var handler = this.CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public bool CanExecute(object parameter)
        {
            return this._canExecute == null ? true : this._canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            if (this.CanExecute(parameter) && this._execute != null)
            {
                this._execute.Invoke();
            }
        }
    }
}
