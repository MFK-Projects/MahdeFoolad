using MahdeFooladWPF.ViewModels;
using MahdeFooladWPF.Views;
using MaterialDesignThemes.Wpf;
using NSMangament.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MahdeFooladWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static MainWindowViewModel _viewModel;
        private static Duration _duration = new(TimeSpan.FromSeconds(0.6));
        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = _viewModel = viewModel;
            systemTray.DataContext = _viewModel;
            leastnotyborder.DataContext = _viewModel.LeastTimeNotification;
            btnchanges.DataContext = _viewModel;
        }


        private void TopStackPanel_MouseDowned(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.FindLeastTimeNotification?.Execute(null);
            leastnotyborder.DataContext = _viewModel.LeastTimeNotification;

        }

        private void CloseCommand(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void NormlizedCommand(object sender,RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            this.Visibility = Visibility.Visible;
            this.Activate();
        }
    }
}
