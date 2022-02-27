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

        private static CustomMessageboxResult Result = CustomMessageboxResult.No;
        public CustomMessageBox(string content, IconImage? icon, Window owner)
        {

            InitializeComponent();
            InitlaElements(content, icon, owner);
        }

        private void InitlaElements(string content, IconImage? icon, Window owner)
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

        public static void ShowMessage(string content, IconImage icon, Window owner)
        {
            var btn1 = new Button
            {
                Content = "بلی",
                Background = new SolidColorBrush(Color.FromRgb(29, 210, 176)),
                Foreground = Brushes.White,
                Margin = new Thickness(30, 0, 15, 0),
                BorderBrush = Brushes.Transparent,
                Width = 100
            };

            var windowo = new CustomMessageBox(content, icon, owner);
            windowo.stackpanelBtns.Children.Add(btn1);
            btn1.Click += (sender, e) => { Result = CustomMessageboxResult.Yes; windowo.Close(); };
            windowo.ShowDialog();
        }


        public static CustomMessageboxResult ShowConfrimMessage(string content, IconImage? icon, CustomMessageBoxButton? button, Window owner)
        {
            if (button == null)
                button = CustomMessageBoxButton.YesOrNo;
            if (icon == null)
                icon = IconImage.Warning;


            var window = new CustomMessageBox(content, icon, owner);


            if (button == CustomMessageBoxButton.YesOrNo)
            {
                var btn1 = new Button
                {
                    Content = "بلی",
                    Background = new SolidColorBrush(Color.FromRgb(29, 210, 176)),
                    Foreground = Brushes.White,
                    Margin = new Thickness(30, 0, 15, 0),
                    BorderBrush = Brushes.Transparent
                };

                var btn2 = new Button
                {
                    Content = "خیر",
                    Background = new SolidColorBrush(Color.FromRgb(110, 230, 10)),
                    Foreground = Brushes.White,
                    BorderBrush = Brushes.Transparent
                };


                btn1.Click += (sender, e) => { Result = CustomMessageboxResult.Yes; window.Close(); };
                btn2.Click += (sender, e) => { Result = CustomMessageboxResult.No; window.Close(); };

                window.stackpanelBtns.Children.Add(btn1);
                window.stackpanelBtns.Children.Add(btn2);

                window.ShowDialog();

                return Result;
            }
            else if (button == CustomMessageBoxButton.Ok)
            {
                var btn1 = new Button
                {
                    Content = "بلی",
                    Background = new SolidColorBrush(Color.FromRgb(110, 230, 10)),
                    Foreground = Brushes.White,
                    Margin = new Thickness(30, 0, 15, 0)
                };

                btn1.Click += (sender, e) => { Result = CustomMessageboxResult.Ok; window.Close(); };
                window.stackpanelBtns.Children.Add(btn1);
                window.ShowDialog();

                return Result;
            }

            return Result;
        }




    }



    public enum IconImage
    {

        Success,
        Failer,
        Warning
    }

    public enum CustomMessageBoxButton
    {

        Ok,
        YesOrNo,
        Cancel,
        YesCancel
    }

    public enum CustomMessageboxResult
    {
        Yes,
        No,
        Cancel,
        Ok
    }


    public enum CustomDialogBoxResult
    {
        Warning,
        Information,
        Error
    }
}
