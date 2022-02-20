using MahdeFooladWPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private static Duration _duration = new (TimeSpan.FromSeconds(0.6));
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ToggleBtn_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animation;
            QuarticEase easing = new();
            easing.EasingMode = EasingMode.EaseInOut;


            if (Stpanel.Width == 260)
            {
                animation = new(45, _duration);
                toggleBtnContent.Kind = MaterialDesignThemes.Wpf.PackIconKind.ArrowRight;
            }
            else
            {
                animation = new(260, _duration);
                toggleBtnContent.Kind = MaterialDesignThemes.Wpf.PackIconKind.ArrowLeft;
            }


            animation.EasingFunction = easing;
            Stpanel.BeginAnimation(StackPanel.WidthProperty, animation);
        }

        private void TopStackPanel_MouseDowned(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void OpTasksWidnowBtn_Click(object sender, RoutedEventArgs e)
        {
            var taskswindow = new TasksListView();
            taskswindow.Owner = this;
            taskswindow.ShowDialog();
        }
    }
}
