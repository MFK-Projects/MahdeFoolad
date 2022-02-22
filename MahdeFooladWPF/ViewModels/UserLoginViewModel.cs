using System.Windows.Input;
using MahdeFooladWPF.Commands;
using NSMangament.Application.Models;
using NSMangament.Application.Services;
using Serilog;

namespace MahdeFooladWPF.ViewModels
{
    public class UserLoginViewModel : BaseViewModel
    {
        private readonly ICommand _loginCommand;
        private IUserMananger _userManager;

        public string UserName => _userManager.User.UserName;

        public ICommand LoginCommand { get => _loginCommand; }

        public UserLoginViewModel(ILogger logger, IUserMananger usermanager)
        {
            _userManager = usermanager;
            _loginCommand = new LoginCommand(logger, usermanager);
        }


    }
}
