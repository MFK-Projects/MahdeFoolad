using Serilog;
using System.Windows;
using Serilog.Sinks.File;
using System;
using Microsoft.Extensions.DependencyInjection;
using NSMangament.Application.Services;
using NSManagament.Infrastrucure.Impelementions;
using MahdeFooladWPF.Views;
using MahdeFooladWPF.ViewModels;

namespace MahdeFooladWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private static string _loginPath = AppDomain.CurrentDomain.BaseDirectory + @"\ApplicationLoggin\log_file.txt";

        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            _logger = new LoggerConfiguration()
                          .WriteTo.File(_loginPath, rollingInterval: RollingInterval.Day)
                          .CreateLogger();

            _serviceProvider = new ServiceCollection()
                                    .AddTransient<IWebRequest, WebRequestService>(x => new WebRequestService(_logger))
                                    .AddSingleton<IUserMananger, UserManagerService>(x => new UserManagerService(x.GetRequiredService<IWebRequest>()))
                                    .BuildServiceProvider();

        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow mainWindow;
            LoginWindow login;
            var _userService = _serviceProvider.GetService<IUserMananger>();

            if (string.IsNullOrEmpty(_userService.User.Password))
            {
                login = new(new UserLoginViewModel(_logger, _userService));
                login.ShowDialog();

                mainWindow = new MainWindow();
                mainWindow.ShowDialog(); 
            }
            else
            {
                mainWindow = new();
                mainWindow.ShowDialog();
            }
        }
    }
}
