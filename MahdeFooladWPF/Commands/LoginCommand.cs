﻿using System;
using System.Windows.Input;
using Serilog;
using NSMangament.Application.Services;
using System.Windows;
using MahdeFooald.Common;

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

        public void Execute(object parameter)
        {
            if (_userMananger.CheckPassword(parameter.ToString()).Result)
                _userMananger.SetUserInfo(new NSMangament.Application.Models.UserModel
                {
                    FullName = _userMananger.User.FullName,
                    Password = parameter.ToString(),
                    UserId = _userMananger.User.UserId,
                    UserName = _userMananger.User.UserName
                });
        }
    }
}