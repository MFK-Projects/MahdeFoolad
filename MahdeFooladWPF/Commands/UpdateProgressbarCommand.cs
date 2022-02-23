using System;
using System.Windows.Input;

namespace MahdeFooladWPF.Commands
{
    public class UpdateProgressbarCommand : ICommand
    {

        private Action _action;
        public event EventHandler CanExecuteChanged;


        public UpdateProgressbarCommand(Action action)
        {
            _action = action;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action?.Invoke();
        }
    }
}
