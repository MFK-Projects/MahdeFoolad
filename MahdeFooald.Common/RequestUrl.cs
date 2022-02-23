using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahdeFooald.Common
{
    public static class RequestUrl
    {
        private const string baseurl = "http://crm-srv:8585/MFKian/api/data/v9.0/";
        private const string taskurl = "http://crm-srv:8585/MFkian/main.aspx?etn=";
        private const string _taskSelectedItem = @"activityid,new_remained_time_hour,
                                        new_remaining_days,new_task_status,new_task_type,subject,prioritycode,
                                        description,new_telephone,_new_date_deadline_value,_ownerid_value,new_notification_threshold";
        private const string _settingCrmSelectedItem = @"new_api_url,new_baseurl,new_day_filter,new_domain_name,new_entity_name,
                                                         new_expired_task_code,new_log_path,new_name,new_notification_message,
                                                         new_notification_systemid,new_setting_timer,new_task_api,new_task_status,
                                                         new_task_types,new_time_awaited,new_time_check";
        private const string _userSelectedItem = "fullname,identityid";
        public const string DomainName = "KIAN";


        public static string BuildUrl(UrlBuilderMode mode, string ownerId, string tasksId,string userId)
        {
            if (mode == UrlBuilderMode.SingleTask && string.IsNullOrEmpty(tasksId))
                throw new ArgumentNullException("userid or taskid sent null to the method ");
            else if (mode == UrlBuilderMode.MultipuleTasks && string.IsNullOrEmpty(userId))
                throw new ArgumentNullException("userid or taskid sent null to the method ");
            else if (mode == UrlBuilderMode.SingleUser && string.IsNullOrEmpty(ownerId))
                throw new ArgumentNullException("userid or taskid sent null to the method ");


            return mode switch
            {
                UrlBuilderMode.Setting => $"{baseurl}new_notification_systems?$select={_settingCrmSelectedItem}",
                UrlBuilderMode.SingleTask => $"{taskurl}task&id={tasksId}&pagetype=entityrecord#",
                UrlBuilderMode.MultipuleTasks => $"{baseurl}tasks?$select={_taskSelectedItem}&$filter=_ownerid_value eq {userId}",
                UrlBuilderMode.SingleUser => $"{baseurl}systemusers?$select={_userSelectedItem}&$filter=domainname eq '{ownerId}'",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

    }

    public enum UrlBuilderMode
    {
        MultipuleTasks,
        SingleUser,
        Setting,
        SingleTask
    }
}


