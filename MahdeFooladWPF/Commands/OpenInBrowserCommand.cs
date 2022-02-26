

using System;
using System.Windows.Input;

namespace MahdeFooladWPF.Commands
{
    public class OpenInBrowserCommand : ICommand
    {
        private readonly Action<object> _openInBrowser;
        public event EventHandler CanExecuteChanged;


        public OpenInBrowserCommand(Action<object> action)
        {
            _openInBrowser = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _openInBrowser.Invoke(parameter);
        }
    }
}
