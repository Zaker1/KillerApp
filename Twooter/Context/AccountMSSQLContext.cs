using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twooter.Models;
using System.Data.SqlClient;
using Twooter.Interfaces;
using Twooter.ViewModels;
using System.Data;
using Twooter.Klassen;

namespace Twooter.Context
{
    public class AccountContext : IAccountContext
    {
        private string ConnectionString = Properties.Resources.ConnectionString;
        public AccountContext()
        {

        }

        public Account Select(Account account)
        {
            //string query = "SELECT AccountID ,[e-mail] ,wachtwoord ,rol_id ,Rol.naam FROM Account INNER JOIN Rol ON Account.rol_id = Rol.Id WHERE [e-mail] = @naam AND wachtwoord = @ww";
            string query = "dbo.spGetUser";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {

                DbContext.OpenConnection(connection);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = account.Email;
                cmd.Parameters.AddWithValue("@Wachtwoord", SqlDbType.NVarChar).Value = account.Wachtwoord;

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            account.ID = Convert.ToInt32(reader["AccountID"]);
                            Rol rol = new Rol
                            {
                                Id = Convert.ToInt32(reader["rol_id"]),
                                Naam = reader["Rol"].ToString()
                            };
                            account.Rol = rol;
                            account.Banned = Convert.ToBoolean(reader["banned"]);
                        }
                    }
                    else
                    {
                        throw new RowNotInTableException();
                    }
                }
                catch (SqlException e)
                {
                    throw Exceptions.FoutQuery(e);
                }
            }
            return account;
        }
    
        public int SelectID(Account account)
        {
            string query = "SELECT AccountID FROM dbo.Account WHERE [e-mail] = @param1";
            int id = 0;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                DbContext.OpenConnection(connection);

                cmd.Parameters.AddWithValue("@param1", account.Email);
                try
                {
                    id = (int)cmd.ExecuteScalar();
                }
                catch (SqlException e)
                {
                    throw Exceptions.FoutQuery(e);
                }
            }
            return id;
        }

        public void Insert(Registreren registreer)
        {
            string query = "dbo.spCreateUser";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                DbContext.OpenConnection(connection);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", SqlDbType.NVarChar).Value = registreer.Gebruikersnaam;
                cmd.Parameters.AddWithValue("@password", SqlDbType.NVarChar).Value = registreer.Wachtwoord;
                cmd.Parameters.AddWithValue("@e_mail", SqlDbType.NVarChar).Value = registreer.Email;

                var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                if ((int)returnParameter.Value == -6)
                {
                    throw new DuplicateNameException();
                }
            }
        }
    }
}
