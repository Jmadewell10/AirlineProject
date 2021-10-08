using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject.Data
{
    public class PassengerDAO
    {
        private string connString = "Data Source=JONATHAN-PC;Initial Catalog=Airline;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public IEnumerable<Passenger> GetPassengers()
        {
            List<Passenger> passengerList = new List<Passenger>();

            using (SqlConnection conn = new SqlConnection(connString))
            {

                conn.Open();

                //Only want to show name, dob, email, and job title.
                string query = "SELECT PassengerName, PassengerDateOfBirth, PassengerEmail, PassengerJobTitle FROM dbo.Passengers;";

                SqlTransaction transaction = conn.BeginTransaction("T1");
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.Transaction = transaction;


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
                        Passenger temp = new Passenger(reader["PassengerName"].ToString(), reader["PassengerDateOfBirth"].ToString(), reader["PassengerEmail"].ToString(), reader["PassengerJobTitle"].ToString());
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Could not get all Passengers!\n{0}", e.Message);
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
                conn.Open();

                string query = "SELECT * FROM dbo.Passengers WHERE id = @id";

                SqlTransaction transaction = conn.BeginTransaction("T1");
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.Transaction = transaction;

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
                        passenger = new Passenger();

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
                conn.Open();
                string query = @"INSERT INTO dbo.Passengers (PassengerName, PassengerEmail, PassengerJobTitle, PassengerDateOfBirth, ConfirmationNumber) OUTPUT INSERTED.Id  values (@PassengerName, @PassengerEmail, @PassengerJobTitle, @PassengerDateOfBirth, @ConfirmationNumber)";

                SqlTransaction transaction = conn.BeginTransaction("T1");
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.Transaction = transaction;
                cmd.Parameters.AddWithValue("@PassengerName", passenger.name);
                cmd.Parameters.AddWithValue("@PassengerEmail", passenger.email);
                cmd.Parameters.AddWithValue("@PassengerJobTitle", passenger.jobTitle);
                cmd.Parameters.AddWithValue("@PassengerDateOfBirth", passenger.dob);
                cmd.Parameters.AddWithValue("@ConfirmationNumber", passenger.confirmationNumber);

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
    }
}
