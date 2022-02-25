using MahdeFooladWPF.ModelConverters;
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
    /// Interaction logic for TasksListView.xaml
    /// </summary>
    public partial class TasksListView : Window
    {
        private readonly TaskListViewModel _vmModel;
        public TasksListView(TaskListViewModel vmModel)
        {
            InitializeComponent();
            _vmModel = vmModel;
            this.DataContext = _vmModel;
     
        }

        private void TaskListWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _vmModel?.RetriveDataCommand.Execute(null);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            _vmModel.DetailConverter.Invoke(taskListBox.SelectedItem as TaskModelConverter);
            detailpanel.DataContext = _vmModel.SingleTask;

            CircleEase easing = new CircleEase();
            easing.EasingMode = EasingMode.EaseOut;
            DoubleAnimation anim = new DoubleAnimation(280, new Duration(TimeSpan.FromMilliseconds(400)));
            detailpanel.BeginAnimation(Border.HeightProperty, anim);
        }
    }
}
