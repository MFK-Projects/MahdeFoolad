using MahdeFooald.Common;
using MahdeFooladWPF.Commands;
using MahdeFooladWPF.ModelConverters;
using MahdeFooladWPF.Views;
using NSManagament.Infrastrucure.Services;
using NSMangament.Application.Models;
using NSMangament.Application.Services;
using Serilog;
using System;
using System.Collections.Generic;
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
        private readonly INotificationService _notificationService;
        private List<TaskModel> lstTasks = new List<TaskModel>();
        private static readonly object _locker = new object();
        private readonly System.Timers.Timer _retriveTimer = new();
        private readonly System.Timers.Timer _notificationTimer = new();
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

        public MainWindowViewModel(ILogger logger, IUtilityService utilityService, IUserMananger userMananger, INotificationService notificationServcie)
        {
            _utilityService = utilityService;
            _userManager = userMananger;
            _logger = logger;
            _notificationService = notificationServcie;
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
                },_logger);
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
                var vmModel = new TaskListViewModel(_utilityService, NSMangament.Application.Enums.TaskListType.WholeList,_logger);
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
                lock (_locker)
                {
                    lstTasks = FilterNotifyTaskService.FilterNotify(_utilityService.RetriveData().Result);


                    if (lstTasks.Count > 0)
                    {
                        var task = FilterNotifyTaskService.LeastTimeNotification(FilterNotifyTaskService.FilterNotify(lstTasks));

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

                            if (taskcount == lstTasks.Count)
                                return false;
                            else
                            {
                                taskcount = lstTasks.Count;
                                return true;
                            }
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
                var vmModel = new TaskListViewModel(_utilityService, NSMangament.Application.Enums.TaskListType.NotifyList,_logger);
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

                Task.Factory.StartNew(() =>
                {

                    _notificationService.SendNotification(new CreationNotificationModel
                    {
                        AurgmentData = "http://80.210.16.4:8585",
                        Button = null,
                        NotificationArgument = "http://80.210.16.4:8585",
                        TaskUrl = null,
                        Text = new string[] { $"{_userManager.User.FullName} خوش آمدید" },
                        ToastDuration = Microsoft.Toolkit.Uwp.Notifications.ToastDuration.Short,
                        ToastScenario = Microsoft.Toolkit.Uwp.Notifications.ToastScenario.Alarm,
                        Titel = String.Empty
                    });

                    _notificationService.SendNotification(new CreationNotificationModel
                    {
                        AurgmentData = String.Empty,
                        Button = null,
                        NotificationArgument = String.Empty,
                        TaskUrl = null,
                        Text = new string[] { $" وظایف شما برای امروز  {lstTasks.Count} " },
                        ToastDuration = Microsoft.Toolkit.Uwp.Notifications.ToastDuration.Short,
                        ToastScenario = Microsoft.Toolkit.Uwp.Notifications.ToastScenario.Alarm,
                        Titel = String.Empty
                    });


                    _notificationTimer.AutoReset = true;
                    _notificationTimer.Interval = 180000;
                    _notificationTimer.Elapsed += _notificationTimer_Elapsed;
                    _notificationTimer.Start();


                    _retriveTimer.AutoReset = true;
                    _retriveTimer.Interval = _rndInterval;
                    _retriveTimer.Elapsed += _retriveTimer_Elapsed;
                    _retriveTimer.Start();
                }).Wait(3000);

                _notificationTimer_Elapsed(null,null);

            }
            catch (Exception ex)
            {
                _logger.Equals($"Error happend in the GetLeastTiemNotification Main Window error Message :{ex.Message} Inner Exception :{ex.InnerException?.Message}");
                CustomMessageBox.ShowMessage(ErrorMessages.DefaultError, IconImage.Failer, null);
            }
        }

        private void _notificationTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (lstTasks.Count > 0)
                {
                    var lstcreatinalnotification = new List<CreationNotificationModel>();

                    foreach (var item in lstTasks)
                    {
                        if (item.TaskStatus == "100000003" || item.TaskStatus == "100000005")
                            continue;

                        lstcreatinalnotification.Add(new CreationNotificationModel
                        {
                            AurgmentData = null,
                            Button = null,
                            NotificationArgument = null,
                            TaskUrl = RequestUrl.BuildUrl(UrlBuilderMode.SingleTask, null, item.ActivityId, null),
                            Text = new string[] { item.Subject, item.Description, $"زمان باقی مانده {item.RemaingHour}" }
                        });
                    }



                    _notificationService.SendNotification(lstcreatinalnotification);
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"error happend while running the send notification method  Error Message :{ex.Message} \t Inner exception :{ex.InnerException?.Message}");
            }
        }

        private void _retriveTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            FillTaksCollection();
        }
    }
}
