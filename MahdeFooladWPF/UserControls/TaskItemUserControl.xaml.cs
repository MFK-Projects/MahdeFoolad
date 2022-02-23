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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MahdeFooladWPF.UserControls
{
    public partial class TaskItemUserControl : UserControl
    {

        public ICommand LoadedCommand
        {
            get { return (ICommand)GetValue(LoadedEventProperty); }
            set { SetValue(LoadedEventProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadedEventProperty =
            DependencyProperty.Register("MyProperty", typeof(ICommand), typeof(TaskItemUserControl), new PropertyMetadata(null));

        public TaskItemUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (LoadedCommand != null) LoadedCommand.Execute(null);
        }
    }
}
