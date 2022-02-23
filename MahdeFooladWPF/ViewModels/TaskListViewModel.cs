

using MahdeFooladWPF.Commands;
using System.Windows;
using System.Windows.Input;

namespace MahdeFooladWPF.ViewModels
{
    public class TaskListViewModel
    {
        public ICommand CloseCommand { get; set; }

        public TaskListViewModel()
        {
            CloseCommand = new CloseCommand(Close);
        }


        private void Close(object paramter)
        {
            var curentWindow = paramter as Window;

            curentWindow.Close();
        }
    }
}
