using Newtonsoft.Json;

namespace NSMangament.Application.Models
{
    //should we get task status and tasktype from the differnt entity for better data handleing and better featuers?

    public class CrmSettingModel
    {
        [JsonProperty(propertyName: "new_entity_name")]
        public string EntityName { get; set; }

        //Check for this Property if its not reqired remove it.
        [JsonProperty(propertyName: "new_notification_systemid")]
        public string NotificationSystemId { get; set; }

        [JsonProperty(propertyName: "new_setting_timer")]
        public byte DailySendCount  { get; set; }


        [JsonProperty(propertyName: "new_task_status")]
        public string TaskStatus { get; set; }

        [JsonProperty(propertyName: "new_task_types")]
        public string TaskTypes { get; set; }


        [JsonProperty(propertyName: "new_api_url")]
        public string BaseRequestUrl { get; set; }

        [JsonProperty(propertyName: "new_domain_name")]
        public string DomainName { get; set; }

        [JsonProperty(propertyName: "new_task_api")]
        public string BaseEntityUrl { get; set; }

    }

    
}
