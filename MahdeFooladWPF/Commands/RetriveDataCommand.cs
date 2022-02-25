using NSMangament.Application.Services;
using System;
using System.Windows.Input;



namespace MahdeFooladWPF.Commands
{
    public class RetriveDataCommand : ICommand
    {
        private Action ExcuteCommand;

        public event EventHandler CanExecuteChanged;
        public RetriveDataCommand(Action excute)
        {
            ExcuteCommand = excute;
        }



        public bool CanExecute(object parameter)
        {
            return true;
        }

        public  void Execute(object parameter)
        {
            ExcuteCommand?.Invoke();
        }
    }
}
