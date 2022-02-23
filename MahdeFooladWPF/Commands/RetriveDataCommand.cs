using NSMangament.Application.Services;
using System;
using System.Windows.Input;



namespace MahdeFooladWPF.Commands
{
    public class RetriveDataCommand : ICommand
    {
        private Action<object> ExcuteCommand;

        public event EventHandler CanExecuteChanged;
        public RetriveDataCommand(Action<object> excute)
        {
            ExcuteCommand = excute;
        }



        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ExcuteCommand?.Invoke(null);
        }
    }
}
