using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSMangament.Application.Models
{
    public class ApplicationSettingModel
    {
        public long[] TaskTypes { get; set; }
        public long[] TaskStatus { get; set; }
        public int ExpiredTaskStatus { get; set; }
        public string BaseTaskUrl { get; set; }
        public string BaseApiUrl { get; set; }
        public byte ToastSentHour { get; set; }
        public byte TaostSentDay { get; set; }
        public string DomainName { get; set; }
        public string EntityName { get; set; }
    }


}
