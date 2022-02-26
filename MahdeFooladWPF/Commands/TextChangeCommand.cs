using System;
using System.Windows.Input;

namespace MahdeFooladWPF.Commands
{
    public class TextChangeCommand : ICommand
    {

        private readonly Action<string> _textChange;
        public event EventHandler CanExecuteChanged;


        public TextChangeCommand(Action<string> action)
        {
            _textChange = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _textChange.Invoke(parameter as string);
        }
    }
}
