using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahdeFooladWPF.ViewModels
{
    internal class TaskViewModel : BaseViewModel
    {

        private string _activityId;
        public string ActivityId
        {
            get => _activityId;
            set { _activityId = value; OnPropertyChaned(nameof(ActivityId)); }
        }
    }
}
