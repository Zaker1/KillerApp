using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Twooter.Models;
using System.Data.SqlClient;
using Twooter.Interfaces;

namespace Twooter.Context
{
    public class ProfielMSSQLContext : IProfielContext
    {
        public ProfielMSSQLContext()
        {

        }
        public byte[] SelectFoto(Profiel profiel)
        {
            string query = "SELECT foto FROM dbo.Gebruiker WHERE AccountID = @param1";
            byte[] fotoArray = null;

            using (SqlConnection connection = new SqlConnection(DbContext.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                DbContext.OpenConnection(connection);

                cmd.Parameters.AddWithValue("@param1", profiel.Account.ID);

                try
                {
                    fotoArray = (byte[])cmd.ExecuteScalar();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return fotoArray;
        }
        public byte[] SelectFoto(int Id)
        {
            string query = "SELECT foto FROM dbo.Gebruiker WHERE GebruikerID = @param1";
            byte[] fotoArray = null;

            using (SqlConnection connection = new SqlConnection(DbContext.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                DbContext.OpenConnection(connection);

                cmd.Parameters.AddWithValue("@param1", Id);

                try
                {
                    fotoArray = (byte[])cmd.ExecuteScalar();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return fotoArray;
        }
        public void InsertBeschrijving(Profiel profiel)
        {
            string query = "INSERT INTO dbo.Gebruiker(beschrijving) VALUES(@param1) WHERE AccountID = @param2";

            using (SqlConnection connection = new SqlConnection(DbContext.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                DbContext.OpenConnection(connection);

                cmd.Parameters.AddWithValue("@param1", profiel.Beschrijving);
                cmd.Parameters.AddWithValue("@param2", profiel.Account.ID);
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
        public Profiel Select(Profiel profiel)
        {
            string query = "SELECT * FROM dbo.Gebruiker WHERE AccountID = @param1";

            using (SqlConnection connection = new SqlConnection(DbContext.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                DbContext.OpenConnection(connection);

                cmd.Parameters.AddWithValue("@param1", profiel.Account.ID);
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        profiel.Gebruikersnaam = dr["gebruikersnaam"].ToString();
                        profiel.Beschrijving = dr["beschrijving"].ToString();
                        profiel.ID = Convert.ToInt32(dr["GebruikerID"]);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return profiel;
        }
        public Profiel Select(int Id)
        {
            string query = "SELECT * FROM dbo.Gebruiker WHERE GebruikerID = @param1";
            Profiel profiel = new Profiel();

            using (SqlConnection connection = new SqlConnection(DbContext.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                DbContext.OpenConnection(connection);

                cmd.Parameters.AddWithValue("@param1", Id);
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        profiel.Gebruikersnaam = dr["gebruikersnaam"].ToString();
                        profiel.Beschrijving = dr["beschrijving"].ToString();
                        profiel.ID = Convert.ToInt32(dr["GebruikerID"]);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return profiel;
        }

        public void Volg(int VolgendId, int VolgerID)
        {
            string query = "INSERT INTO dbo.Volgers(VolgerID1, VolgendID2) VALUES(@param1, @param2)";

            using (SqlConnection connection = new SqlConnection(DbContext.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                DbContext.OpenConnection(connection);

                cmd.Parameters.AddWithValue("@param1", VolgerID);
                cmd.Parameters.AddWithValue("@param2", VolgendId);
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

        public void UnVolg(int VolgendId, int VolgerID)
        {
            string query = "DELETE dbo.Volgers WHERE VolgerID1 = @param1 AND VolgendID2 = @param2";

            using (SqlConnection connection = new SqlConnection(DbContext.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                DbContext.OpenConnection(connection);

                cmd.Parameters.AddWithValue("@param1", VolgerID);
                cmd.Parameters.AddWithValue("@param2", VolgendId);
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

        public List<Profiel> Select()
        {
            List<Profiel> profielen = new List<Profiel>();

            string query = "SELECT * FROM dbo.Gebruiker";

            using (SqlConnection connection = new SqlConnection(DbContext.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                DbContext.OpenConnection(connection);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Profiel profiel = new Profiel
                        {
                            ID = Convert.ToInt32(reader["GebruikerID"]),
                            Gebruikersnaam = reader["gebruikersnaam"].ToString(),
                            Beschrijving = reader["beschrijving"].ToString(),
                        };

                        if (reader["foto"] != DBNull.Value)
                        {
                            profiel.Foto = (byte[])reader["foto"];
                        }
                        profiel.Account = new Account { ID = Convert.ToInt32(reader["AccountID"]) };

                        profielen.Add(profiel);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return profielen;
        }

        public void Report(Report report)
        {
            string query = "INSERT INTO dbo.Report(reported_id, reporter_id, bericht, datum) VALUES(@reported, @reporter, @bericht, @datum)";

            using (SqlConnection connection = new SqlConnection(DbContext.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();

                cmd.Parameters.AddWithValue("@reported", report.Reported_id);
                cmd.Parameters.AddWithValue("@reporter", report.Reporter_id);
                cmd.Parameters.AddWithValue("@bericht", report.Bericht);
                cmd.Parameters.AddWithValue("@datum", report.Tijd);

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

        public List<Profiel> GetReportedProfiel()
        {
            string query = "SELECT GebruikerID, gebruikersnaam, COUNT(dbo.Report.reported_id) AS 'Reports' " +
                           "FROM dbo.Gebruiker LEFT JOIN dbo.Report ON dbo.Report.reported_id = Gebruiker.GebruikerID " +
                           "WHERE Gebruiker.GebruikerID IN (SELECT DISTINCT dbo.Report.reported_id FROM dbo.Report) " +
                           "GROUP BY GebruikerID, gebruikersnaam";
            List<Profiel> profielen = new List<Profiel>();

            using (SqlConnection connection = new SqlConnection(DbContext.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                DbContext.OpenConnection(connection);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Profiel profiel = new Profiel
                        {
                            ID = Convert.ToInt32(reader["GebruikerID"]),
                            Gebruikersnaam = Convert.ToString(reader["gebruikersnaam"]),
                            AantalReports = Convert.ToInt32(reader["Reports"])
                        };
                        profielen.Add(profiel);
                    }
                }
                catch (Exception)
                {

                }
            }
            return profielen;
        }

        public List<Report> GetReportsFromProfiel(int id)
        {
            string query = "SELECT [reported_id], [reporter_id], bericht, datum, Gebruiker.gebruikersnaam AS 'reporterNaam' FROM dbo.Report " +
                "INNER JOIN Gebruiker ON Gebruiker.GebruikerID = [reported_id]" +
                "WHERE [reported_id] = @id";
            List<Report> reports = new List<Report>();

            using (SqlConnection connection = new SqlConnection(DbContext.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                DbContext.OpenConnection(connection);

                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Report report = new Report
                        {
                            Reported_id = Convert.ToInt32(reader["reported_id"]),
                            Reporter_id = Convert.ToInt32(reader["reporter_id"]),
                            Bericht = Convert.ToString(reader["bericht"]),
                            ReporterNaam = Convert.ToString(reader["reporterNaam"]),
                            Tijd = Convert.ToString(reader["datum"])
                        };

                        reports.Add(report);
                    }
                }
                catch (Exception)
                {
                    
                }
            }
            return reports;
        }

        public void Update(Profiel profiel)
        {
            string query = "";

            using (SqlConnection connection = new SqlConnection(DbContext.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                DbContext.OpenConnection(connection);
                try
                {

                }
                catch (Exception)
                {

                }
            }
            
        }

        public void UpdateBan(int id, bool ban)
        {
            string query = "UPDATE Account SET banned = @ban FROM Account INNER JOIN dbo.Gebruiker ON Account.AccountID = Gebruiker.AccountID WHERE GebruikerID = @profielId";

            using (SqlConnection connection = new SqlConnection(DbContext.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                DbContext.OpenConnection(connection);

                cmd.Parameters.AddWithValue("@profielId", id);
                cmd.Parameters.AddWithValue("@ban", ban);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {

                }
            }
            
        }

        public List<Profiel> GetBannedProfiel()
        {
            string query = "SELECT GebruikerID, gebruikersnaam, beschrijving from Gebruiker " +
                           "INNER JOIN Account ON Gebruiker.AccountID = Account.AccountID " +
                           "WHERE Account.banned = 1";
            List<Profiel> profielen = new List<Profiel>();

            using (SqlConnection connection = new SqlConnection(DbContext.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                DbContext.OpenConnection(connection);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Profiel profiel = new Profiel
                        {
                            ID = Convert.ToInt32(reader["GebruikerID"]),
                            Gebruikersnaam = Convert.ToString(reader["gebruikersnaam"]),
                            Beschrijving = Convert.ToString(reader["beschrijving"])
                        };
                        profielen.Add(profiel);
                    }
                }
                catch (Exception)
                {

                }
            }
            return profielen;
            
        }
    }
}
