using MahdeFooladWPF.ViewModels;
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
using System.Windows.Shapes;

namespace MahdeFooladWPF.Views
{
    /// <summary>
    /// Interaction logic for ChangeStatusWindow.xaml
    /// </summary>
    public partial class ChangeStatusWindow : Window
    {
        private readonly ChangeStatusViewModel _vmModel;
        public ChangeStatusWindow(ChangeStatusViewModel vmModel)
        {
            InitializeComponent();
            _vmModel = vmModel;
            this.Width = 0;
            this.Height = 0;
            AnimateWindow();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = cmbStatus.SelectedItem as ComboBoxItem;
            _vmModel?.ChangeStatusCommand.Execute(item.Tag);
        }

        private void AnimateWindow()
        {
            DoubleAnimation animey = new (220, TimeSpan.FromMilliseconds(600));
            DoubleAnimation animex = new (200, TimeSpan.FromSeconds(1));
            CircleEase easing = new();
            
            easing.EasingMode = EasingMode.EaseIn;

            animey.EasingFunction = easing;
            animex.EasingFunction = easing;
            this.BeginAnimation(Window.WidthProperty, animex);
            this.BeginAnimation(Window.HeightProperty, animey);

        }
    }
}
