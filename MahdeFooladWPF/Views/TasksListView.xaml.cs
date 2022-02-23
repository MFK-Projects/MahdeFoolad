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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MahdeFooladWPF.Views
{
    /// <summary>
    /// Interaction logic for TasksListView.xaml
    /// </summary>
    public partial class TasksListView : Window
    {

        private readonly TaskListViewModel _taskListvm = new();
        public TasksListView()
        {
            InitializeComponent();
            this.DataContext = _taskListvm;
        }
    }
}
