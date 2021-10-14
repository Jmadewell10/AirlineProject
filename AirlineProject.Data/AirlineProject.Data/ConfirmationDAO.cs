using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject.Data
{
    public class ConfirmationDAO
    {
        private string connString = "Data Source=JONATHAN-PC;Initial Catalog=Airline;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public IEnumerable<Confirmation> GetConfirmations()
        {
            List<Confirmation> confirmationList = new List<Confirmation>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                //Only want to show name, dob, email, and job title.
                string query = "SELECT * FROM dbo.Confirmations";
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {



                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Confirmation temp = new Confirmation();

                        temp.id = Convert.ToInt32(reader["ConfirmationId"]);
                        temp.flightId = Convert.ToInt32(reader["Flight"]);


                        confirmationList.Add(temp);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get confirmations!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }

                return confirmationList;

            }
        }

        public Confirmation GetConfirmation(int id)
        {
            Confirmation confirmation = new Confirmation();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                string query = "SELECT * FROM dbo.Confirmations WHERE ConfirmationId = @id";

                SqlTransaction transaction = conn.BeginTransaction("T1");
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.Transaction = transaction;
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    int affected = cmd.ExecuteNonQuery();

                    if (affected > 0)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        confirmation = new Confirmation();

                        confirmation.id = id;
                        confirmation.flightId = Convert.ToInt32(reader["Flight"]);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get confirmation\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return confirmation;
            }
        }

        public void AddConfirmation(Confirmation confirmation)
        {
            int id = 0;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                string query = @"INSERT INTO dbo.Confirmations (Flight) OUTPUT INSERTED.PlaneId  values (@Flight)";

                SqlTransaction transaction = conn.BeginTransaction("T1");
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.Transaction = transaction;
                cmd.Parameters.AddWithValue("@Flight", confirmation.flightId);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;

                try
                {
                    int affected = cmd.ExecuteNonQuery();

                    if (affected > 0)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                    id = (int)cmd.Parameters["@Id"].Value;

                    confirmation.id = id;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not add confirmation!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }





            }
        }

        public void DeleteConfirmation(int id)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                string query = "Delete from dbo.Confirmations where ConfirmationId = @Id";

                SqlTransaction transaction = conn.BeginTransaction("T1");
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.Transaction = transaction;
                cmd.Parameters.AddWithValue("@Id", id);

                try
                {
                    int affected = cmd.ExecuteNonQuery();

                    if (affected > 0)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }


                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not delete Confirmation!!\n {0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }


            }
        }

        public void UpdateConfirmation(Confirmation confirmation)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                string query = $"update dbo.Confirmations set Flight = @Flight where ConfirmationId = @id";

                SqlTransaction transaction = conn.BeginTransaction("T1");
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.Transaction = transaction;
                cmd.Parameters.AddWithValue("@Id", confirmation.id);
                cmd.Parameters.AddWithValue("@Flight", confirmation.flightId);


                try
                {
                    int affected = cmd.ExecuteNonQuery();

                    if (affected > 0)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }


                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not update Confirmation!!\n {0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }


            }
        }
    }
}
