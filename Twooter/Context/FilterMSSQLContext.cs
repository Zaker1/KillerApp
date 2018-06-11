using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Twooter.Interfaces;
using Twooter.Models;

namespace Twooter.Context
{
    public class FilterMSSQLContext : IFilterContext
    {
        public void Delete(string filter)
        {
            string query = "DELETE FROM dbo.Filter WHERE woord = @param1";

            using (SqlConnection connection = new SqlConnection(DbContext.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                DbContext.OpenConnection(connection);

                cmd.Parameters.AddWithValue("@param1", filter);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            
        }

        public void Insert(string filter)
        {
            string query = "INSERT INTO dbo.Filter(woord) VALUES(@param1)";

            using (SqlConnection connection = new SqlConnection(DbContext.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                DbContext.OpenConnection(connection);

                cmd.Parameters.AddWithValue("@param1", filter);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }         
        }

        public List<Filter> Select()
        {
            string query = "SELECT * FROM dbo.Filter";
            List<Filter> filters = new List<Filter>();

            using (SqlConnection connection = new SqlConnection(DbContext.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                DbContext.OpenConnection(connection);

                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Filter filter = new Filter
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Woord = reader["woord"].ToString()
                        };
                        filters.Add(filter);
                    }
                }
                catch (SqlException e)
                {
                    Exception exception = new Exception("Query is fout", e);
                    throw exception;
                }
            }
            return filters;
        }
    }
}
