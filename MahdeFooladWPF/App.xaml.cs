using Serilog;
using System.Windows;
using Serilog.Sinks.File;
using System;
using Microsoft.Extensions.DependencyInjection;
using NSMangament.Application.Services;
using NSManagament.Infrastrucure.Impelementions;
using MahdeFooladWPF.Views;
using MahdeFooladWPF.ViewModels;
using System.Threading;

namespace MahdeFooladWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow mainWindow;
        private LoginWindow login;
        private readonly static string _loginPath = AppDomain.CurrentDomain.BaseDirectory + @"\ApplicationLoggin\log_file.txt";
        private MainWindowViewModel _mainViewModel;
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            _logger = new LoggerConfiguration()
                          .WriteTo.File(_loginPath, rollingInterval: RollingInterval.Day)
                          .CreateLogger();

            _serviceProvider = new ServiceCollection()
                                    .AddSingleton<IWebRequest, WebRequestService>(x => new WebRequestService(_logger))
                                    .AddSingleton<IUserMananger, UserManagerService>(x => new UserManagerService(x.GetRequiredService<IWebRequest>()))
                                    .AddScoped<IUtilityService, UtilityService>(x => new UtilityService(x.GetRequiredService<IWebRequest>(), x.GetRequiredService<IUserMananger>()))
                                    .BuildServiceProvider();

        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {

            var _userService = _serviceProvider.GetService<IUserMananger>();
            var _uilityService = _serviceProvider.GetService<IUtilityService>();
            _mainViewModel = new MainWindowViewModel(_logger,_uilityService,_userService);

            if (string.IsNullOrEmpty(_userService.User.Password))
            {
                login = new(new UserLoginViewModel(_logger, _userService));
                login.ShowDialog();

                if (login.IsClosed)
                    return;

                mainWindow = new(_mainViewModel);
                mainWindow.ShowDialog();
            }
            else
            {
                mainWindow = new(_mainViewModel);
                mainWindow.ShowDialog();
            }
        }
    }
}
