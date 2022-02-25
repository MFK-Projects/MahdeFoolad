

using MahdeFooladWPF.Commands;
using MahdeFooladWPF.ModelConverters;
using MahdeFooladWPF.Views;
using NSMangament.Application.Models;
using NSMangament.Application.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MahdeFooladWPF.ViewModels
{
    public class TaskListViewModel : ObservableCollection<TaskModelConverter>
    {
        private static TaskModelConverter _taskdetails = new ();
        private readonly IUtilityService _utilityService;
        public ICommand CloseCommand { get; set; }
        public ICommand OpenChangeStatusWindow { get; set; }
        public ICommand RetriveDataCommand { get; set; }
        public ICommand ShowDetailCommand { get; set; }
        public Action<TaskModelConverter> DetailConverter;
        public TaskModelConverter SingleTask
        {
            get => _taskdetails;
            set
            {
                _taskdetails = value;
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(SingleTask)));
            }
        }


        public TaskListViewModel(IUtilityService utilityService)
        {
            _utilityService = utilityService;
            NewMethod();
        }

        private void NewMethod()
        {
            CloseCommand = new CloseCommand(Close);
            RetriveDataCommand = new RetriveDataCommand(GetAllData);
            DetailConverter = new Action<TaskModelConverter>(ShowDetail);
        }

        private void OpenChangeTaskWindow(object paramter)
        {
            var vmModel = new ChangeStatusViewModel();
            ChangeStatusWindow window = new ChangeStatusWindow(vmModel);

            window.ShowDialog();
        }

        private void ShowDetail(TaskModelConverter model)
        {
            _taskdetails = model;
        }
        private void GetAllData()
        {
            var lst = _utilityService.RetriveData().Result;

            foreach (var item in lst)
            {
                var task = new TaskModelConverter();
                task.FullDescription = item.Description;
                task.Subject = item.Subject;
                task.TaskStatus = item.TaskStatus;
                task.TaskType = item.TaskType;
                task.RemainginHour = item.RemaingHour.ToString();
                task.RemainingDays = DateTime.Now.AddDays(item.RemainingDay).ToString("yyyy/MM/dd");
                task.TaskId = item.ActivityId;

                TaskCoditions(item, task);

                Add(task);
            }

        }
        private static void TaskCoditions(TaskModel item, TaskModelConverter task)
        {

            if (item.Description.Length > 40)
                task.MiniDescription = item.Description.Substring(0, 40) + "...";
            else
                task.MiniDescription = item.Description;

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
            var curentWindow = paramter as Window;
            curentWindow.Close();
        }
    }
}
