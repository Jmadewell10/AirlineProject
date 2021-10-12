using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject.Data
{
    public class PassengerDAO : IPassengerDAO
    {
        private string connString = "Data Source=JONATHAN-PC;Initial Catalog=Airline;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public IEnumerable<Passenger> GetPassengers()
        {
            List<Passenger> passengerList = new List<Passenger>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                //Only want to show name, dob, email, and job title.
                string query = "SELECT * FROM dbo.Passengers";
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {

                       
                    
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Passenger temp = new Passenger(reader["PassengerName"].ToString(), reader["PassengerDateOfBirth"].ToString(), reader["PassengerEmail"].ToString(), reader["PassengerJobTitle"].ToString());

                        temp.id = Convert.ToInt32(reader["PassengerId"]);

                        passengerList.Add(temp);
                    }
                }
                catch(SqlException ex)
                {
                    Console.WriteLine("Could not get passengers!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }

                return passengerList;
            }
        }

        public Passenger GetPassenger(int id)
        {
            Passenger passenger = new Passenger();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                string query = "SELECT * FROM dbo.Passengers WHERE PassengerId = @id";

                SqlTransaction transaction = conn.BeginTransaction("T1");
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.Transaction = transaction;
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    int affected = cmd.ExecuteNonQuery();

                    if(affected > 0)
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
                        passenger = new Passenger(reader["PassengerName"].ToString(), reader["PassengerDateOfBirth"].ToString(), reader["PassengerEmail"].ToString(), reader["PassengerJobTitle"].ToString());

                        passenger.id = id;
                    }
                }catch(SqlException ex)
                {
                    Console.WriteLine("Could not get passenger\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return passenger;
            }
        }

        public void AddPassenger(Passenger passenger)
        {
            int id = 0;
            
            using(SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                string query = @"INSERT INTO dbo.Passengers (PassengerName, PassengerEmail, PassengerJobTitle, PassengerDateOfBirth, PassengerConfirmationNumber) OUTPUT INSERTED.Id  values (@PassengerName, @PassengerEmail, @PassengerJobTitle, @PassengerDateOfBirth, @PassengerConfirmationNumber)";

                SqlTransaction transaction = conn.BeginTransaction("T1");
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.Transaction = transaction;
                cmd.Parameters.AddWithValue("@PassengerName", passenger.name);
                cmd.Parameters.AddWithValue("@PassengerEmail", passenger.email);
                cmd.Parameters.AddWithValue("@PassengerJobTitle", passenger.jobTitle);
                cmd.Parameters.AddWithValue("@PassengerDateOfBirth", passenger.dob);
                cmd.Parameters.AddWithValue("@PassengerConfirmationNumber", passenger.confirmationNumber);
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

                    passenger.id = id;
                }catch(SqlException ex)
                {
                    Console.WriteLine("Could not add passenger!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }





            }
        }

        public void DeletePassenger(int id)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                string query = "Delete from dbo.Passengers where PassengerId = @Id";

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


                }catch(SqlException ex)
                {
                    Console.WriteLine("Could not delete passenger!!\n {0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }


            }
        }

        public void UpdatePassenger(Passenger passenger)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                string query = $"update dbo.Passengers set PassengerDateOfBirth = @dob, PassengerEmail = @email, PassengerJobTitle = @job, PassengerName = @name where PassengerId = @id";

                SqlTransaction transaction = conn.BeginTransaction("T1");
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.Transaction = transaction;
                cmd.Parameters.AddWithValue("@Id", passenger.id);
                cmd.Parameters.AddWithValue("@dob", passenger.dob);
                cmd.Parameters.AddWithValue("@email", passenger.email);
                cmd.Parameters.AddWithValue("@job", passenger.jobTitle);
                cmd.Parameters.AddWithValue("@name", passenger.name);

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
                    Console.WriteLine("Could not delete passenger!!\n {0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }


            }
        }
    }
}
