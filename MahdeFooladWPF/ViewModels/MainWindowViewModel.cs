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
        private static int taskcount = default;
        private readonly ILogger _logger;
        private readonly IUtilityService _utilityService;
        private ICommand _retriveDataCommand;
        private ICommand _taskListCommand;
        private readonly IUserMananger _userManager;
        private NotificationModelConverter _leastTimeNotification;
        private readonly System.Timers.Timer _retriveTimer = new ();
        private readonly double _rndInterval = new Random().Next(60000, (60000 * 3));
        #endregion

        public NotificationModelConverter LeastTimeNotification
        {
            get => _leastTimeNotification;
            set
            {
                _leastTimeNotification = value;
                OnPropertyChaned(nameof(LeastTimeNotification));
            }
        }

        #region Commands
        public ICommand CloseCommand { get; set; }
        public ICommand MinimizedCommand { get; set; }
        public ICommand UpdateProggressBarCommand { get; set; }
        public ICommand FindLeastTimeNotification { get; set; }
        public ICommand OpenNotificationTaskWindow { get; set; }
        public ICommand RestDataCommand => _retriveDataCommand;
        public ICommand TaskListCommand => _taskListCommand;
        public ICommand NormalizedWindowCommand { get; set; }
        public ICommand OpenChangeWidnowCommand { get; set; }
        #endregion
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
            MinimizedCommand = new MinizedWidnowCommand(WindowMinimized);
            _retriveDataCommand = new RetriveDataCommand(GetData);
            _taskListCommand = new TaskListCommand(OpenTaskListWindow);
            CloseCommand = new CloseCommand(WindowClose);
            OpenNotificationTaskWindow = new OpenWindowCommand(OpenNotiyWindow);
            FindLeastTimeNotification = new FindLeastTimeNotificationCommnad(GetLeastTimeNotification);
            OpenChangeWidnowCommand = new OpenWindowCommand(OpenChangesWindow);
        }

        private void OpenChangesWindow(object obj)
        {
            try
            {
                if (string.IsNullOrEmpty(obj as string))
                    return;

                var vmModel = new ChangeStatusViewModel(_utilityService, new TaskModelConverter
                {
                    TaskId = obj as string
                });
                ChangeStatusWindow window = new(vmModel);
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, "happned while executing the OpenChangeWindow");
                CustomMessageBox.ShowMessage("با پشتیانی تماس بگیرید", IconImage.Failer, null);
            }
        }


        private void OpenTaskListWindow(object paramter)
        {
            try
            {
                var vmModel = new TaskListViewModel(_utilityService, NSMangament.Application.Enums.TaskListType.WholeList);
                var tasklistwindow = new TasksListView(vmModel);

                tasklistwindow.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error happend while exceuting the OpenTasklistWindow : {ex.Message} Inner exception :{ex.InnerException?.Message}");
                CustomMessageBox.ShowMessage("لطفا با پشتیبانی تماس بگیرید", IconImage.Warning, null);
            }
        }
        private void GetData()
        {
            try
            {
                if (FillTaksCollection())
                    CustomMessageBox.ShowMessage("اطلاعات با موفقیت به روز رسانی شد", IconImage.Success, null);
                else
                    CustomMessageBox.ShowMessage("اطلاعات شما به روز می باشد", IconImage.Failer, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message + "\n Happend while updateing data in ResetDataCommand In main window");
                CustomMessageBox.ShowMessage(ErrorMessages.DefaultError, IconImage.Failer, null);
            }
        }
        private bool FillTaksCollection()
        {
            try
            {
                var checkdata = _utilityService.RetriveData().Result;


                if (checkdata != null)
                {
                    var task = FilterNotifyTaskService.LeastTimeNotification(FilterNotifyTaskService.FilterNotify(checkdata));

                    if (task != null)
                    {
                        _leastTimeNotification = new NotificationModelConverter
                        {
                            Description = task.Description ?? "توضیحاتی  ندارد‍",
                            TaskUrl = RequestUrl.BuildUrl(UrlBuilderMode.SingleTask, null, task.ActivityId, null),
                            Title = task.Subject,
                            RemainigDate = DateTime.Now.AddDays(task.RemainingDay).ToString("yyyy/MM/dd"),
                            RemainingTime = task.RemaingHour,
                            TaskId = task.ActivityId
                        };

                        if (taskcount == checkdata.Count)
                            return false;
                        else
                        {
                            taskcount = checkdata.Count;
                            return true;
                        }

                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message + "Error ouccer in the fillCollection Method while runing detail are explained in the curent explaing");
                return false;
            }
        }
        private void WindowMinimized(object paramter)
        {
            if (paramter == null)
                return;


            var window = paramter as Window;
            window.WindowState = WindowState.Minimized;
            window.Visibility = Visibility.Hidden;
        }
        private void WindowClose(object paramter)
        {
            if (paramter == null)
                return;

            var window = paramter as Window;
            window.Close();
        }
        private void OpenNotiyWindow(object paramter)
        {
            try
            {
                var vmModel = new TaskListViewModel(_utilityService, NSMangament.Application.Enums.TaskListType.NotifyList);
                var taskWindow = new TasksListView(vmModel);
                taskWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error ouccre in the OpenNotifyWindow and Error Details are :  {ex.Message}");
                CustomMessageBox.ShowMessage("لطفا با پشتیبانی تماس بگیرید ", IconImage.Failer, null);
            }
        }
        private void GetLeastTimeNotification(object paramter)
        {
            try
            {
                FillTaksCollection();

                _retriveTimer.AutoReset = true;
                _retriveTimer.Interval = _rndInterval;
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
