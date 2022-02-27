using MahdeFooladWPF.Commands;
using MahdeFooladWPF.Views;
using NSMangament.Application.Models;
using NSMangament.Application.Services;
using Serilog;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace MahdeFooladWPF.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Private Fields
        private readonly ILogger _logger;
        private readonly IUtilityService _utilityService;
        private ICommand _retriveDataCommand;
        private ICommand _taskListCommand;
        private readonly IUserMananger _userManager;
        #endregion


        public ICommand CloseCommand { get; set; }
        public ICommand MinimizedCommand { get; set; }
        public ICommand UpdateProggressBarCommand { get; set; }
        public ICommand RetriveDataCommand { get; set; }
        public ICommand OpenNotificationTaskWindow { get; set; }
        public ICommand RestDataCommand => _retriveDataCommand;
        public ICommand TaskListCommand => _taskListCommand;


        public string FullName { get => _userManager.User.FullName; }

        public MainWindowViewModel(ILogger logger, IUtilityService utilityService, IUserMananger userMananger)
        {
            _utilityService = utilityService;
            _userManager = userMananger;
            _logger = logger;
            RegisterEvents();
        }


        private void RegisterEvents()
        {
            UpdateProggressBarCommand = new UpdateProgressbarCommand(UpdateProgressbar);
            MinimizedCommand = new MinizedWidnowCommand(WindowMinimized);
            _retriveDataCommand = new RetriveDataCommand(GetData);
            _taskListCommand = new TaskListCommand(OpenTaskListWindow);
            CloseCommand = new CloseCommand(WindowClose);
            OpenNotificationTaskWindow = new OpenWindowCommand(OpenNotiyWindow);
        }
        private void OpenTaskListWindow(object paramter)
        {
            var vmModel = new TaskListViewModel(_utilityService, NSMangament.Application.Enums.TaskListType.WholeList);
            var tasklistwindow = new TasksListView(vmModel);

            tasklistwindow.ShowDialog();
        }
        private void GetData()
        {
            if (FillTaksCollection())
                MessageBox.Show("عملیات با موفقیت انجام شد");
        }
        private bool FillTaksCollection()
        {
            var checkdata = _utilityService.RetriveData();

            if (checkdata.Result != null)
            {
                return true;
            }

            return false;
        }
        private void WindowMinimized(object paramter)
        {
            var window = paramter as Window;
            window.WindowState = WindowState.Minimized;
            window.Visibility = Visibility.Hidden;
        }
        private void WindowClose(object paramter)
        {
            var window = paramter as Window;
            window.Close();
        }
        private void UpdateProgressbar(object paramter)
        {
        }
        private void OpenNotiyWindow(object paramter)
        {
            var vmModel = new TaskListViewModel(_utilityService, NSMangament.Application.Enums.TaskListType.NotifyList);
            var taskWindow = new TasksListView(vmModel);
            taskWindow.ShowDialog();
        }


        /*
        Task<List<TaskModel>> MostRecentTasks();
        Task<List<TaskModel>> ExpiredTasks();
        Task<List<TaskModel>> HighPriorityTasks();
        TaskModel MostPriorityTask();
        TaskModel ExpireingTask();*/
    }
}
