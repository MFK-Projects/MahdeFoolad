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
        }

        private void ToggleBtn_Click(object sender, RoutedEventArgs e)
        {

            DoubleAnimation animation;
            QuarticEase easing = new();
            PackIcon icons = new();


            easing.EasingMode = EasingMode.EaseInOut;

            if (Stpanel.Width == 260)
            {
                icons.Kind = PackIconKind.ArrowRight;
                ToggleBtn.Content = icons;
                animation = new(45, _duration);
            }
            else
            {
                icons.Kind = PackIconKind.ArrowLeft;
                ToggleBtn.Content = icons;
                animation = new(260, _duration);
            }


            animation.EasingFunction = easing;
            Stpanel.BeginAnimation(StackPanel.WidthProperty, animation);

        }

        private void TopStackPanel_MouseDowned(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                _viewModel.UpdateProggressBarCommand?.Execute(progressb);
            });
            
            

            _viewModel.RetriveDataCommand?.Execute(null);
        }
    }
}
