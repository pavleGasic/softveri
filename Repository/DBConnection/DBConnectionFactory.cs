using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DBConnection
{
    public class DBConnectionFactory
    {

        private static DBConnectionFactory _instance;
        private readonly DBConnection _connection;
        public static DBConnectionFactory Instance { get { if (_instance == null) _instance = new DBConnectionFactory(); return _instance; } }
        private DBConnectionFactory()
        {
            _connection = new DBConnection();
        }
        public DBConnection GetDBConnection()
        {
            if (!_connection.IsReady())
            {
                _connection.OpenConnection();
            }
            return _connection;
        }

    }
}
