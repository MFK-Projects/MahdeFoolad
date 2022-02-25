using MahdeFooladWPF.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace MahdeFooladWPF.ViewModels
{
    public class ChangeStatusViewModel : BaseViewModel
    {

        public ICommand ChangeStatusCommand { get; set; }
        public ICommand ClsoeCommand { get; set; }
        public ChangeStatusViewModel()
        {
            ClsoeCommand = new CloseCommand(CloseWindow);
            ChangeStatusCommand = new ChangeStatusCommand(ChangeTaskStatus);
        }



        private void ChangeTaskStatus(object paramter)
        {
            int data = (int)paramter;
        }
        private void CloseWindow(object paramter)
        {
            var window = paramter as Window;

            window.Close();
        }
    }
}
