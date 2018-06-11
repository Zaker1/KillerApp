using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twooter.Models;
using System.Data.SqlClient;
using System.Data;
using Twooter.Interfaces;

namespace Twooter.Context
{
    public class TijdlijnMSSQLContext : ITijdlijnContext
    {
        
        public TijdlijnMSSQLContext()
        {

        }
        public List<Twoot> SelectFavoTwoot(Tijdlijn tijdlijn)
        {
            string query = "SELECT * FROM Twoot INNER JOIN Favoriete_twoot ON Twoot.GebruikerID = Favoriete_twoot.GebruikerID AND Twoot.TweetID = Favoriete_twoot.TweetID WHERE Favoriete_twoot.GebruikerID = @param1";
            List<Twoot> favoTwoots = new List<Twoot>();

            using (SqlConnection connection = new SqlConnection(DbContext.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                DbContext.OpenConnection(connection);

                cmd.Parameters.AddWithValue("@param1", tijdlijn.Profiel.ID);
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Twoot twoot = new Twoot
                        {
                            ID = Convert.ToInt32(reader["TweetID"]),
                            Bericht = reader["bericht"].ToString(),
                            ProfielID = Convert.ToInt32(reader["GebruikerID"]),
                            date = Convert.ToDateTime(reader["datum"]),
                            Favoriet = true
                        };

                        favoTwoots.Add(twoot);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return favoTwoots;
        }

        public List<Twoot> SelectTwoot(Tijdlijn tijdlijn)
        {
            string query = "dbo.spGetTwoots";
            List<Twoot> twoots = new List<Twoot>();

            using (SqlConnection connection = new SqlConnection(DbContext.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                DbContext.OpenConnection(connection);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProfielID", SqlDbType.Int).Value = tijdlijn.Profiel.ID;
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    DataTable tableA = ds.Tables[0];
                    DataTable tableB = ds.Tables[1];


                    foreach (DataRow dr in tableA.Rows)
                    {
                        Twoot twoot = new Twoot
                        {
                            ID = Convert.ToInt32(dr["TweetID"]),
                            Bericht = dr["bericht"].ToString(),
                            ProfielID = Convert.ToInt32(dr["GebruikerID"]),
                            date = Convert.ToDateTime(dr["datum"]),
                            naamPoster = dr["gebruikersnaam"].ToString()
                        };

                        if (dr.IsNull("favoriet"))
                        {
                            twoot.Favoriet = false; 
                        }
                        else
                        {
                            twoot.Favoriet = true;
                        }
                        twoots.Add(twoot);
                    }

                    foreach (DataRow dr in tableB.Rows)
                    {
                        Twoot twoot = new Twoot
                        {
                            ID = Convert.ToInt32(dr["TweetID"]),
                            Bericht = dr["bericht"].ToString(),
                            ProfielID = Convert.ToInt32(dr["GebruikerID"]),
                            date = Convert.ToDateTime(dr["datum"]),
                            naamPoster = dr["gebruikersnaam"].ToString()
                        };

                        if (dr.IsNull("favoriet"))
                        {
                            twoot.Favoriet = false;
                        }
                        else
                        {
                            twoot.Favoriet = true;
                        }

                        twoots.Add(twoot);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return twoots.OrderByDescending(x => x.date).ToList();
        }

        public List<Profiel> SelectVriend(Tijdlijn tijdlijn)
        {
            string query = "SELECT * FROM dbo.Gebruiker WHERE GebruikerID IN (SELECT VolgendID2 FROM dbo.Volgers WHERE VolgerID1 = @param1)";
            List<Profiel> profielen = new List<Profiel>();

            using (SqlConnection connection = new SqlConnection(DbContext.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                DbContext.OpenConnection(connection);

                cmd.Parameters.AddWithValue("@param1", tijdlijn.Profiel.ID);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    byte[] ba =  null;
                    if (reader["foto"] != DBNull.Value)
                    {
                        ba = (byte[])reader["foto"];
                    }
                    Profiel profielVriend = new Profiel
                    {
                        Gebruikersnaam = reader["gebruikersnaam"].ToString(),
                        Foto = ba,
                        ID = Convert.ToInt32(reader["GebruikerID"]),
                        Beschrijving = reader["beschrijving"].ToString(),
                        Volgend = true
                    };

                    profielen.Add(profielVriend);
                }
            }
            return profielen;
        }
    }
}
