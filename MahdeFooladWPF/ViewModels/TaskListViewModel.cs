using MahdeFooald.Common;
using MahdeFooladWPF.Commands;
using MahdeFooladWPF.ModelConverters;
using MahdeFooladWPF.Views;
using NSManagament.Infrastrucure.Services;
using NSMangament.Application.Enums;
using NSMangament.Application.Models;
using NSMangament.Application.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace MahdeFooladWPF.ViewModels
{
    public class TaskListViewModel : BaseViewModel
    {
        private static TaskModelConverter _taskdetails = new();
        private readonly IUtilityService _utilityService;
        private string filter = string.Empty;
        private TaskListType _lstType;
        private readonly ILogger _logger;
        private string tsakTypeChange;
        public string TaskType { get; set; }
        public string TaskTypeChange
        {
            get => tsakTypeChange;
            set
            {
                tsakTypeChange = value;
                OnPropertyChaned(nameof(TaskTypeChange));
                ItemsView.Refresh();
                ItemsView.Filter = FilterStatus;
            }
        }
        public ICollectionView ItemsView
        {
            get { return CollectionViewSource.GetDefaultView(TaskCollection); }
        }
        public TaskModelConverter SingleTask
        {
            get => _taskdetails;
            set
            {
                _taskdetails = value;
                OnPropertyChaned(nameof(SingleTask));
            }
        }
        public ObservableCollection<TaskModelConverter> TaskCollection { get; set; }

        public string Filter
        {
            get => filter;
            set
            {
                if (value != filter)
                {
                    filter = value;
                    ItemsView.Refresh();
                    OnPropertyChaned(nameof(Filter));
                    ItemsView.Filter = FilterItems;
                    ItemsView.Refresh();
                }
            }
        }


        #region Commands 
        public ICommand CloseCommand { get; set; }
        public ICommand OpenChangeStatusWindow { get; set; }
        public ICommand RetriveDataCommand { get; set; }
        public ICommand ShowDetailCommand { get; set; }
        public ICommand TextChangeCommand { get; set; }
        public ICommand OpenTaskInChromeCommand { get; set; }

        public Action<TaskModelConverter> DetailConverter;

        #endregion


        public TaskListViewModel(IUtilityService utilityService,TaskListType lsttype,ILogger logger)
        {
            TaskCollection = new();
            _lstType = lsttype;
            _utilityService = utilityService;
            _logger = logger;
            RegisterCommands();

        }

         private void RegisterCommands()
        {
            CloseCommand = new CloseCommand(Close);
            RetriveDataCommand = new RetriveDataCommand(GetAllData);
            DetailConverter = new Action<TaskModelConverter>(ShowDetail);
            OpenChangeStatusWindow = new OpenWindowCommand(OpenChangeTaskWindow);
            OpenTaskInChromeCommand = new OpenInBrowserCommand(OpenChrome);
        }
        private void OpenChangeTaskWindow(object paramter)
        {
            try
            {
                var vmModel = new ChangeStatusViewModel(_utilityService, SingleTask, _logger);
                ChangeStatusWindow window = new(vmModel);
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }
        private void ShowDetail(TaskModelConverter model)
        {
            _taskdetails = model;
        }
        private void GetAllData()
        {
            try
            {
                List<TaskModel> lst;

                if (_lstType == TaskListType.NotifyList)
                    lst = FilterNotifyTaskService.FilterNotify(_utilityService.RetriveData().Result);
                else
                    lst = _utilityService.RetriveData().Result;

                foreach (var item in lst)
                {
                    TaskModelConverter task = new();
                    task.FullDescription = item.Description;
                    task.Subject = item.Subject;
                    task.TaskStatus = item.TaskStatus;
                    task.TaskType = item.TaskType;
                    task.RemainginHour = item.RemaingHour.ToString();
                    task.RemainingDays = DateTime.Now.AddDays((item.RemainingDay - 1)).ToString("yyyy/MM/dd");
                    task.TaskId = item.ActivityId;

                    TaskCoditions(item, task);
                    TaskCollection.Add(task);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }
        private static void TaskCoditions(TaskModel item, TaskModelConverter task)
        {

            if (!string.IsNullOrEmpty(item.Description))
            {
                if (item.Description.Length > 40)
                    task.MiniDescription = item.Description.Substring(0,40);
                else
                    task.MiniDescription = item.Description;
            }
            else
                task.MiniDescription = "توضیحاتی ندارد";

            if (item.PriorityCode == 0)
                task.Prioroty = "کم";
            else if (item.PriorityCode == 2)
                task.Prioroty = "بالا";
            else if (item.PriorityCode == 1)
                task.Prioroty = "نرمال";


            if (item.RemaingHour < 0)
                task.RemainginHour = "به اتمام رسیده است";
        }
        private void Close(object paramter)
        {
            TaskCollection.Clear();
            var curentWindow = paramter as Window;
            curentWindow.Close();
        }

        private bool FilterItems(object paramter)
        {
            var task = paramter as TaskModelConverter;

            if (string.IsNullOrEmpty(Filter))
                return true;

            return task.Subject.Contains(Filter);


        }
        private bool FilterStatus(object paramter)
        {
            var task = paramter as TaskModelConverter;

            if (TaskTypeChange == "0")
                return true;
            else if (tsakTypeChange == "1" && task.TaskStatus == "100000003")
                return true;
            else if (TaskTypeChange == "2" && task.TaskStatus == "100000004")
                return true;
            else if (TaskTypeChange == "3" && task.TaskStatus == "100000004")
                return true;
            else if (TaskTypeChange == "4" && task.TaskStatus == "100000002")
                return true;

            return false;
        }
        private void OpenChrome(object paramter)
        {
            try
            {
                string taskid = paramter as string;

                if (string.IsNullOrEmpty(taskid))
                    return;

                Task.Run(() =>
                {
                    string url = RequestUrl.BuildUrl(UrlBuilderMode.SingleTask, null, taskid, null);
                    System.Diagnostics.Process.Start(new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
