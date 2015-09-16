using System;
using System.Windows.Input;

namespace RolerFramework.Command
{
    public class RolerCommand<T> : ICommand where T : class
    {
        #region Feilds

        private readonly Action<T> _execute;

        private readonly Func<T, bool> _canExecute;

        #endregion

        #region Constructor

        public RolerCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public RolerCommand(Action<T> execute, Func<T, bool> canExecute)
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
            if (this._canExecute == null)
            {
                return true;
            }
            return this._canExecute.Invoke((T)parameter);
        }

        public void Execute(object parameter)
        {
            if (this.CanExecute(parameter) && this._execute != null)
            {
                _execute.Invoke((T)parameter);
            }
        }
    }
}
