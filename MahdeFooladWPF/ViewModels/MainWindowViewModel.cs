using MahdeFooladWPF.Commands;
using MahdeFooladWPF.Views;
using NSMangament.Application.Models;
using NSMangament.Application.Services;
using Serilog;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;


namespace MahdeFooladWPF.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ICommand _retriveDataCommand;
        private ICommand _taskListCommand;
        private readonly ILogger _logger;
        private IUtilityService _utilityService;
        private TaskModel _curentTask;
        private TaskModel CurentTask { get => _curentTask; set => _curentTask = value; }
        private ObservableCollection<TaskModel> tasklist = new ObservableCollection<TaskModel>();


        public ICommand RestDataCommand => _retriveDataCommand;
        public ICommand TaskListCommand => _taskListCommand;

        public ObservableCollection<TaskModel> Tasks { get => tasklist; set => tasklist = value; }

        public MainWindowViewModel(ILogger logger, IUtilityService utilityService)
        {
            _logger = logger;
            _utilityService = utilityService;
            _retriveDataCommand = new RetriveDataCommand(GetData);
            _taskListCommand = new TaskListCommand(OpenTaskListWindow);
        }


        private void OpenTaskListWindow(object paramter)
        {
            FillTaksCollection();

            new TasksListView(this).ShowDialog();
        }
        private void GetData(object paramter)
        {


            if (FillTaksCollection())
                MessageBox.Show("عملیات با موفقیت انجام شد");


        }




        private bool FillTaksCollection()
        {
            var checkdata = _utilityService.RetriveData();

            if (checkdata.Result != null)
           {
                foreach (var item in checkdata.Result)
                    if (tasklist.Where(x => x.ActivityId != item.ActivityId) != null)
                        tasklist.Add(item);
                return true;
            }

            return false;
        }
    }
}
