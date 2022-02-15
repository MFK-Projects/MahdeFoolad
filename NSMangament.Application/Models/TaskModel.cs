using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSMangament.Application.Models
{
    public class TaskModel
    {
        [JsonProperty("TEntity")]
        public string Subject { get; set; }

        [JsonProperty("New_task_type")]
        public string TaskType { get; set; }

        [JsonProperty("New_task_status")]
        public string TaskStatus { get; set; }

        [JsonProperty("_ownerid_value")]
        public string OwnerId { get; set; }

        [JsonProperty("new_remained_time_hour")]
        public string RemaingHour { get; set; }

        [JsonProperty("new_remaining_days")]
        public byte RemainingDay { get; set; }

        [JsonProperty("activityid")]
        public string ActivityId { get; set; }

        [JsonProperty("_new_date_deadline_value")]
        public string DeadlineValue { get; set; }

        [JsonProperty("prioritycode")]
        public string PriorityCode { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("new_date_deadline_Task")]
        public TaskDeadLine DeadLine { get; set; }

        [JsonProperty("TaskRate")]
        public double TaskRate { get; set; }
    }

    
    public class TaskDeadLine
    {
        [JsonProperty("new_name")]
        public string DeadLineValue { get; set; }
    }
}
