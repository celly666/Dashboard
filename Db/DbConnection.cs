using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace _Dashboard.Db
{
    public abstract class DbConnection
    {
        private readonly string connectionString;
        public DbConnection()
        {
            connectionString = "Server = (local); DataBase = NorthwindStore; Integrated Security = true";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
