using MahdeFooald.Common;
using MahdeFooladWPF.Commands;
using MahdeFooladWPF.ModelConverters;
using MahdeFooladWPF.Views;
using NSMangament.Application.Services;
using Serilog;
using System;
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
        private readonly ILogger _logger;
        public ICommand ChangeStatusCommand { get; set; }
        public ICommand ClosedCommand { get; set; }
        public ChangeStatusViewModel(IUtilityService utilityService, TaskModelConverter task, ILogger logger)
        {
            RegisterCommands();
            _logger = logger;
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
            try
            {
                if (paramter == null)
                    MessageBox.Show("باید یک وضعیت جدید انتحا کنید");
                else
                {
                    var value = (paramter as ComboBoxItem).Tag.ToString();
                    _ = _utilityService.UpdateData(value, _task.TaskId);

                    CustomMessageBox.ShowMessage("عملیات باموفقیت انجام شد", IconImage.Success, null);
                }
            }
            catch(Exception ex)
            {
                _logger.Error($"error happend while excuting the ChangeTaskStatus {ex.Message} ");
                CustomMessageBox.ShowMessage(ErrorMessages.DefaultError, IconImage.Failer, null);
            }
        }
        private void CloseWindow(object paramter)
        {
            var window = paramter as Window;

            if (window != null)
                window.Close();
            else
                MessageBox.Show("Error while Closeing the window please contact to the supporter");
        }
    }
}
