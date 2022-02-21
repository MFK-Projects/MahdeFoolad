using System.Windows.Input;
using MahdeFooladWPF.Commands;
using NSMangament.Application.Services;
using Serilog;

namespace MahdeFooladWPF.ViewModels
{
    public class UserLoginViewModel : BaseViewModel
    {
        private readonly ICommand _loginCommand;
        private string _user;


        public string User
        {
            get => _user;
            set { _user = value; OnPropertyChaned(nameof(User)); }
        }


        public ICommand LoginCommand { get => _loginCommand; }

        public UserLoginViewModel(ILogger logger, IUserMananger usermanager)
        {
            _loginCommand = new LoginCommand(logger, usermanager);
        }


    }
}
