using System;
using System.Windows.Input;

namespace MahdeFooladWPF.Commands
{
    public class OpenWindowCommand : ICommand
    {
        private readonly Action<object> _openWindow;
        public event EventHandler CanExecuteChanged;

        public OpenWindowCommand(Action<object> action)
        {
            _openWindow = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;    
        }

        public void Execute(object parameter)
        {
            _openWindow.Invoke(parameter);
        }
    }
}
