using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ThreadPoolR_Boczoń
{
    public class ComandClass : ICommand
    {
        Action<object> execute;
        Func<object, bool> canExecute;

        public ComandClass(Action<object> e, Func<object,bool> ce)
        {
            if (e != null)
            {
                execute = e;
                canExecute = ce;
            }
            else
                throw new NotImplementedException();
        }





        public event EventHandler CanExecuteChanged
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

        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
                return false;
            else
                return true;
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
