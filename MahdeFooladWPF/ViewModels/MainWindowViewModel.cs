using MahdeFooald.Common;
using MahdeFooladWPF.Commands;
using MahdeFooladWPF.ModelConverters;
using MahdeFooladWPF.Views;
using NSManagament.Infrastrucure.Services;
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
        private NotificationModelConverter _leastTimeNotification;
        private readonly System.Timers.Timer _retriveTimer = new System.Timers.Timer();
        private readonly double _rndInterval = new Random().Next(60000, (60000 * 3));
        #endregion

        public NotificationModelConverter LeastTimeNotification 
        {
            get =>_leastTimeNotification;
            set
            {
                _leastTimeNotification = value;
                OnPropertyChaned(nameof(LeastTimeNotification));
            }
        }


        public ICommand CloseCommand { get; set; }
        public ICommand MinimizedCommand { get; set; }
        public ICommand UpdateProggressBarCommand { get; set; }
        public ICommand FindLeastTimeNotification { get; set; }
        public ICommand OpenNotificationTaskWindow { get; set; }
        public ICommand RestDataCommand => _retriveDataCommand;
        public ICommand TaskListCommand => _taskListCommand;
        public ICommand NormalizedWindowCommand { get; set; }

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
            NormalizedWindowCommand = new NormilizedWindowCommand(ChagneWindowState);
            FindLeastTimeNotification = new FindLeastTimeNotificationCommnad(GetLeastTimeNotification);
        }

        private void ChagneWindowState(object obj)
        {
            var window = obj as Window;
            window.WindowState = WindowState.Normal;
            window.Visibility = Visibility.Visible;
            window.Activate();
        }

        private void OpenTaskListWindow(object paramter)
        {
            var vmModel = new TaskListViewModel(_utilityService, NSMangament.Application.Enums.TaskListType.WholeList);
            var tasklistwindow = new TasksListView(vmModel);

            tasklistwindow.ShowDialog();
        }
        private void GetData()
        {
            try
            {

                if (FillTaksCollection())
                    CustomMessageBox.ShowMessage("اطلاعات با موفقیت به روز رسانی شد", IconImage.Success, null);
                else
                    CustomMessageBox.ShowMessage("هیج  دیتای یافت نشد", IconImage.Failer, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message + "\n Happend while updateing data in ResetDataCommand In main window");
                CustomMessageBox.ShowMessage(ErrorMessages.DefaultError, IconImage.Failer, null);
            }
        }
        private bool FillTaksCollection()
        {
            var checkdata = _utilityService.RetriveData().Result;

            if (checkdata != null)
            {
                var task = FilterNotifyTaskService.LeastTimeNotification(FilterNotifyTaskService.FilterNotify(checkdata));
                
                if (task != null)
                {
                    _leastTimeNotification = new NotificationModelConverter
                    {
                        Description = task.Description,
                        TaskUrl = RequestUrl.BuildUrl(UrlBuilderMode.SingleTask, null, task.ActivityId, null),
                        Title = task.Subject,
                        RemainigDate = DateTime.Now.AddDays(task.RemainingDay).ToString("yyyy/MM/dd"),
                        RemainingTime = task.RemaingHour
                    };

                    return true;
                }
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
        private void GetLeastTimeNotification(object paramter)
        {
            try
            {
                _retriveTimer.AutoReset = true;
                _retriveTimer.Interval = 40000;
                _retriveTimer.Elapsed += _retriveTimer_Elapsed;
                _retriveTimer.Start();
            }
            catch (Exception ex)
            {
                _logger.Equals($"Error happend in the GetLeastTiemNotification Main Window error Message :{ex.Message} Inner Exception :{ex.InnerException?.Message}");
                CustomMessageBox.ShowMessage(ErrorMessages.DefaultError, IconImage.Failer, null);
            }
        }

        private void _retriveTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            FillTaksCollection();
        }
    }
}
