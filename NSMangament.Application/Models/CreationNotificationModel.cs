using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;


namespace NSMangament.Application.Models
{
    public class CreationNotificationModel
    {
        public string Titel { get; set; }
        public string[] Text { get; set; }
        public ToastScenario ToastScenario { get; set; }
        public ToastDuration ToastDuration { get; set; }
        public List<ToastButton> Button { get; set; }
        public string TaskUrl { get; set; }
        public string NotificationArgument { get; set; }
    }
}
