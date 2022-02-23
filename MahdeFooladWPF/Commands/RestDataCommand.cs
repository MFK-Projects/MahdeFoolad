using NSMangament.Application.Services;
using System;
using System.Windows.Input;
using MahdeFooald.Common;
using System.Windows;

namespace MahdeFooladWPF.Commands
{
    class RestDataCommand : ICommand
    {
        private readonly IUserMananger _userManager;
        private readonly IWebRequest _webRequest;
        public RestDataCommand(IUserMananger userManager, IWebRequest webRequest)
        {
            _userManager = userManager;
            _webRequest = webRequest;
        }


        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var result = MessageBox.Show("باز گردانی تنظیمات به حالت اول ؟",string.Empty,MessageBoxButton.YesNo,MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes) return;
        }
    }
}
