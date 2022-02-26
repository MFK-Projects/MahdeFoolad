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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MahdeFooladWPF.Views
{
    /// <summary>
    /// Interaction logic for CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {

        private static CustomDialogBoxResult Result;
        public CustomMessageBox(string content ,IconImage icon,Window owner)
        {
           
            InitializeComponent();
            InitlaElements(content, icon,owner);
        }

        private void InitlaElements(string content, IconImage icon,Window owner)
        {
            if (owner == null)
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            else
            {
                this.Owner = owner;
                this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            if (icon == IconImage.Failer)
            {
                xIocn.Kind = MaterialDesignThemes.Wpf.PackIconKind.Error;
                txtMessage.Text = content;
                xbackground.Fill = new SolidColorBrush(Color.FromRgb(218, 30, 30));

            }
            else if (icon == IconImage.Warning)
            {
                xIocn.Kind = MaterialDesignThemes.Wpf.PackIconKind.Warning;
                txtMessage.Text = content;
                xbackground.Fill = new SolidColorBrush(Color.FromRgb(250, 210, 2));
            }
            else if (icon == IconImage.Success)
            {
                xIocn.Kind = MaterialDesignThemes.Wpf.PackIconKind.Done;
                txtMessage.Text = content;
                xbackground.Fill = new SolidColorBrush(Color.FromRgb(52, 220, 110));
            }
        }

        public static void ShowMessage(string content, IconImage icon,Window owner)
        {
            var windowo = new CustomMessageBox(content, icon,owner);
            windowo.ShowDialog();
        }

   

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }



    public enum IconImage { 
    
        Success,
        Failer,
        Warning
    }


    public enum CustomDialogBoxResult
    {
        Warning,
        Information,
        Error
    }
}
