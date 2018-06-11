using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Twooter.Context
{
    public static class DbContext
    {
        public static string GetConnectionString()
        {
            return Properties.Resources.ConnectionString;
        }

        public static void OpenConnection(SqlConnection conn)
        {
            try
            {
                conn.Open();
            }
            catch (SqlException)
            {
                throw;
            }
        }
    }
}
