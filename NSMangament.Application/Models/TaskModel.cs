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
        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("New_task_type")]
        public string TaskType { get; set; }

        [JsonProperty("New_task_status")]
        public string TaskStatus { get; set; }

        [JsonProperty("_ownerid_value")]
        public string OwnerId { get; set; }

        [JsonProperty("new_remained_time_hour")]
        public int RemaingHour { get; set; }

        [JsonProperty("new_remaining_days")]
        public int RemainingDay { get; set; }

        [JsonProperty("activityid")]
        public string ActivityId { get; set; }

        //[JsonProperty("_new_date_deadline_value")]
        //public string DeadlineValue { get; set; }

        [JsonProperty("prioritycode")]
        public int PriorityCode { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }


        //[JsonProperty("TaskRate")]
        //public double TaskRate { get; set; }
        
        [JsonProperty("new_notification_threshold")]
        public float Threshold { get; set; }
    }
}
