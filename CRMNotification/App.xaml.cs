using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CRMNotification
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private static IServiceProvider _serviceProvider;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                _serviceProvider = new ServiceCollection()
                                      .BuildServiceProvider();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
