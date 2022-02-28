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
        public static List<TaskModel> FilterNotify(List<TaskModel> tasks)
        {
            var notifyTask = new List<TaskModel>();

            foreach (var task in tasks)
            {
                if (task.PriorityCode == 2 && task.RemainingDay > 0 && task.RemainingDay < 5)
                {
                    notifyTask.Add(task);
                    continue;
                }
                else if (task.Threshold <= 0.5 && task.Threshold > 0)
                {
                    notifyTask.Add(task);
                    continue;
                }
                else if (task.RemainingDay == -1)
                {
                    notifyTask.Add(task);
                    continue;
                }
            }

            return notifyTask;
        }

        public static TaskModel LeastTimeNotification(List<TaskModel> tasks)
        {

            if(tasks != null)
            {
                if (tasks.Count <= 0)
                    return null;

                double temp = 1;

                foreach (var task in tasks)
                    if (temp > task.Threshold)
                        temp = task.Threshold;

                return tasks.FirstOrDefault(x => x.Threshold == temp);

            }
            return null;

        }
    }
}
