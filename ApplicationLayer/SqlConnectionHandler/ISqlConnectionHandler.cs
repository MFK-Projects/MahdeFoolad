using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.SqlConnectionHandler
{
    public interface ISqlConnectionHandler:IDisposable
    {
        SqlConnection Create();
    }
}
