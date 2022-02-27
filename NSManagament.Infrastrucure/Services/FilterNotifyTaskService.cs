using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSMangament.Application.Models;

namespace NSManagament.Infrastrucure.Services
{
    public static class FilterNotifyTaskService
    {

        private readonly static List<TaskModel> _tasks = new ();
        public  static List<TaskModel> NotifyTasks { get => _tasks; }


        public static void FilterNotify(List<TaskModel> tasks)
        {
            foreach (var task in tasks)
            {
                if (task.PriorityCode == 2 && task.RemainingDay > 0 && task.RemainingDay < 5)
                {
                    _tasks.Add(task);
                    continue;
                }
                else if(task.Threshold <= 0.5 && task.Threshold > 0)
                {
                    _tasks.Add(task);
                    continue;
                }
                else if(task.RemainingDay == -1)
                {
                    _tasks.Add(task);
                    continue;
                }
            }
        }
    }
}
