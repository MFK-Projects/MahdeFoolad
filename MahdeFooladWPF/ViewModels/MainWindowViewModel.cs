using MahdeFooladWPF.Commands;
using NSMangament.Application.Models;
using NSMangament.Application.Services;
using Serilog;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;


namespace MahdeFooladWPF.ViewModels
{
    public class MainWindowViewModel:BaseViewModel
    {
        private ICommand _retriveDataCommand;
        private readonly ILogger _logger;
        private IUtilityService _utilityService;
        private TaskModel _curentTask;
        private TaskModel CurentTask { get => _curentTask; set => _curentTask = value; }


        public ICommand RestDataCommand => _retriveDataCommand;




        public MainWindowViewModel(ILogger logger,IUtilityService utilityService)
        {
            _logger = logger;
            _utilityService = utilityService;
            _retriveDataCommand = new RetriveDataCommand(GetData);
        }






        private void GetData(object paramter)
        {
           var tasklist =  _utilityService.RetriveData();

            if (tasklist.Result != null)
                MessageBox.Show("عملیات با موفقیت انجام شد");
        }
    }
}
