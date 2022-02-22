using NSMangament.Application.Services;
using System;
using System.Windows.Input;


namespace MahdeFooladWPF.Commands
{
    class RestDataCommand : ICommand
    {
        private readonly IUserMananger _userManager;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            
        }
    }
}
