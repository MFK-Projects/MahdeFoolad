
using System;
using System.Windows.Input;

namespace MahdeFooladWPF.Commands
{
    public class NormilizedWindowCommand : ICommand
    {
        private Action<object> _action;
        public event EventHandler CanExecuteChanged;


        public NormilizedWindowCommand(Action<object> action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
           _action.Invoke(parameter);
        }
    }
}
