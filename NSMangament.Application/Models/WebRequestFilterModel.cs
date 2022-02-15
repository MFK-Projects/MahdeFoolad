using NSMangament.Application.Enums;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSMangament.Application.Models
{
    public class WebRequestFilterModel
    {
        public string Item { get; set; }
        public object Value { get; set; }
        public string Key { get; set; }
        public RequestFiltersType Type { get; set; }
    }
}
