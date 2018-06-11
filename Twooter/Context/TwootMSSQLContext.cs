using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twooter.Models;
using System.Data.SqlClient;
using Twooter.Interfaces;
using Twooter.Klassen;

namespace Twooter.Context
{
    public class TwootMSSQLContext : ITwootContext
    {
        private string ConnectionString = Properties.Resources.ConnectionString;
        public TwootMSSQLContext()
        {

        }
        public void InsertTwoot(Twoot twoot)
        {
            string query = "INSERT INTO dbo.Twoot(bericht, GebruikerID, datum) VALUES(@param1, @param2, @param3)";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                DbContext.OpenConnection(connection);

                cmd.Parameters.AddWithValue("@param1", twoot.Bericht);
                cmd.Parameters.AddWithValue("@param2", twoot.ProfielID);
                cmd.Parameters.AddWithValue("@param3", twoot.date);
                try
                {
                    cmd.ExecuteNonQuery();
                }              
                catch (SqlException e)
                {
                    throw Exceptions.FoutQuery(e);
                }
                catch (Exception a)
                {
                    throw a;
                }
            }
        }
        public void InsertFavorieteTwoot(int twootID, int ProfielID)
        {
            string query = "INSERT INTO dbo.Favoriete_twoot(TweetID, GebruikerID) VALUES(@param1, @param2)";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                DbContext.OpenConnection(connection);

                cmd.Parameters.AddWithValue("@param1", twootID);
                cmd.Parameters.AddWithValue("@param2", ProfielID);
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
        public void DeleteFavorieteTwoot(int twootID, int profielID)
        {
            string query = "DELETE dbo.Favoriete_twoot WHERE TweetID = @param1 AND GebruikerID = @param2";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                DbContext.OpenConnection(connection);

                cmd.Parameters.AddWithValue("@param1", twootID);
                cmd.Parameters.AddWithValue("@param2", profielID);
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
    }
}
