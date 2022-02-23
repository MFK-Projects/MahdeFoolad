using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSMangament.Application.Models
{
    public class RootListModel<TEntity> where TEntity : class
    {
        [JsonProperty("Value")]
        public List<TEntity> Entities { get; set; }
    }
}
