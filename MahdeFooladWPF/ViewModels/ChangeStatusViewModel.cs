using MahdeFooladWPF.Commands;
using MahdeFooladWPF.ModelConverters;
using MahdeFooladWPF.Views;
using NSMangament.Application.Services;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MahdeFooladWPF.ViewModels
{
    public class ChangeStatusViewModel : BaseViewModel
    {
        private TaskModelConverter _task;
        private readonly IUtilityService _utilityService;
        public ICommand ChangeStatusCommand { get; set; }
        public ICommand ClosedCommand { get; set; }
        public ChangeStatusViewModel(IUtilityService utilityService, TaskModelConverter task)
        {
            RegisterCommands();
            _utilityService = utilityService;
            _task = task;
        }

        private void RegisterCommands()
        {
            ChangeStatusCommand = new ChangeStatusCommand(ChangeTaskStatus);
            ClosedCommand = new CloseCommand(CloseWindow);
        }

        private void ChangeTaskStatus(object paramter)
        {
            if (paramter == null)
                MessageBox.Show("باید یک وضعیت جدید انتحا کنید");
            else
            {
                var value = (paramter as ComboBoxItem).Tag.ToString();
                var result = _utilityService.UpdateData(value, _task.TaskId);

                CustomMessageBox.ShowMessage("عملیات باموفقیت انجام شد", IconImage.Success,null);
                
            }

        }
        private void CloseWindow(object paramter)
        {
            var window = paramter as Window;

            window.Close();
        }
    }
}
