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
        public LoginWindow(UserLoginViewModel userloginViewModel)
        {
            InitializeComponent();

            this.DataContext = userloginViewModel;
        }

        private async void btnClose_Click(object sender, RoutedEventArgs e)
        {
            await ShutdownApplication();
        }

        private async Task ShutdownApplication()
        {
            await Task.Delay(2000);
            Application.Current.Shutdown();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(1.5)));
            this.BeginAnimation(Window.OpacityProperty, animation);

            new MainWindow().Show();
            this.Close();
        }
    }
}
