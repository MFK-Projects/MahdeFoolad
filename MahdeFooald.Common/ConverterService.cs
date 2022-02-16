using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace MahdeFooald.Common
{
    public abstract class ConverterService
    {


        public static TEntity ConvertSignleRow<TEntity>(string data) where TEntity : class
        {
            return JsonConvert.DeserializeObject<TEntity>(data);
        }

        public static List<TEntity> ConvertMultipuleRow<TEntity>(string data) where TEntity :class
        {
            return JsonConvert.DeserializeObject<List<TEntity>>(data);
        }
    }
}
