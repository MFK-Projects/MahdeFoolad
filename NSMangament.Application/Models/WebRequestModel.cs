using System.Collections.Generic;

namespace NSMangament.Application.Models
{
    public class WebRequestModel
    {
       public string[] SelectedItem { get; set; }
        public string RequestEntityName { get; set; }
        public short RetriveCount { get; set; }
        public bool AcceptRelatedData { get; set; }
        public WebRequestModel RelatedData { get; set; }
        public List<WebRequestFilterModel> Filters { get; set; }
    }
}
