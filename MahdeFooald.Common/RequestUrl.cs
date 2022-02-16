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

        public static string CrmSettingUrl()
        {
            return "";
        }

        public static string CrmTasksUrl(string userId  )
        {
            return string.Empty;
        }

        public static string CrmTaskUrl(string taskId)
        {
            return string.Empty;
        }
    }
}
