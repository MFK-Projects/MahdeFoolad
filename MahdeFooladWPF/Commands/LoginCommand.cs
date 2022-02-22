using System;
using System.Windows.Input;
using Serilog;
using NSMangament.Application.Services;
using System.Windows;
using MahdeFooald.Common;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.Threading;

namespace MahdeFooladWPF.Commands
{
    internal class LoginCommand : ICommand
    {
        private readonly ILogger _logger;
        private readonly IUserMananger _userMananger;

        public LoginCommand(ILogger logger, IUserMananger userMananger)
        {
            _logger = logger;
            _userMananger = userMananger;
        }

        public event EventHandler CanExecuteChanged;


        public bool CanExecute(object parameter)
        {
            try
            {
                if (string.IsNullOrEmpty(_userMananger.User.Password))
                    return true;


                return false;
            }
            catch (Exception ex)
            {
                _logger.Error($"Erro Code : 320 \n Error happend while application executing in login command class execption Message : {ex.Message} \n inner exception Message :{ex.InnerException?.Message}");
                MessageBox.Show($"erro {ErrorCodes.FAILED_LOGIN_ATTEMPT}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
        }

        public async void Execute(object parameter)
        {
            string password = (parameter as PasswordBox).Password;

            if (await _userMananger.CheckPassword(password))
            {
                _userMananger.User.Password = password;
                bool operationcheck = await _userMananger.RetriveFromCRM();

                if (!operationcheck)
                {
                    MessageBox.Show("Can not retive data from CRM  for curent user please contact with the supporters");
                    return;
                }
                else
                    _userMananger.SetUserInfo(_userMananger.User);
            }
            else
                MessageBox.Show("رمز عبور وارد شده اشتباه است");
        }
    }
}
