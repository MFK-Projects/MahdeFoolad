﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MahdeFooladWPF.Commands
{
    public class TaskListCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<object> ExecuteCommand;

        public TaskListCommand(Action<object> excuteCommand)
        {
            ExecuteCommand = excuteCommand;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ExecuteCommand?.Invoke(null);
        }
    }
}
