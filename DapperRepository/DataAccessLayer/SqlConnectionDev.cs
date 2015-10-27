using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class SqlConnectionDev
    {
        public SqlConnection GetSqlConnection()
        {
            var conn = new SqlConnection();
            conn.ConnectionString =
                @"connection string goes here";
            return conn;
        }
    }
}
