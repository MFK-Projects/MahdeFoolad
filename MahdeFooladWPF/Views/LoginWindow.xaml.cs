using MahdeFooladWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace MahdeFooladWPF.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public bool IsClosed { get; set; }

        public LoginWindow(UserLoginViewModel userloginViewModel)
        {
            InitializeComponent();
            this.Closing += LoginWindow_Closing;
            this.DataContext = userloginViewModel;
        }

        private void LoginWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(2)));
            this.BeginAnimation(Window.OpacityProperty, animation);
        }

        private async void btnClose_Click(object sender, RoutedEventArgs e)
        {
            await ShutdownApplication();
        }

        private async Task ShutdownApplication()
        {
            await Task.Delay(2000);
            IsClosed = true;
            Application.Current.Shutdown();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(async () => 
            {
                await Task.Delay(3000);
                this.Close();
            });
        }
    }
}
