using ApplicationLayer.SqlConnectionHandler;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastrucureLayer.SqlConnectionHandler
{
    public class SqlConnectionHelper: ISqlConnectionHandler
    {
        private static object _lock = new object();
        private SqlConnection _connection;
        private static string _connectionString;
        private bool _disposed = false;

        public SqlConnectionHelper(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException($"{typeof(string) + " " + nameof(connectionString)} can not be null while passing as the paramter in {typeof(SqlConnection)}");

            _connectionString = connectionString;
        }


        public SqlConnection Create()
        {
            lock (_lock)
            {
                if (_connection == null)
                    _connection = new SqlConnection(_connectionString);

                return _connection;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool dispose)
        {
            if (_disposed) return;
            if (dispose)
            {
                _connection?.Dispose();
                _connectionString = null;
            }

            _disposed = true;
        }
    }
}
