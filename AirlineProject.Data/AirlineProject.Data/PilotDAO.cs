using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject.Data
{
    public class PilotDAO : IPilotDAO
    {
        private string connString = "Data Source=JONATHAN-PC;Initial Catalog=Airline;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public IEnumerable<Pilot> GetPilots()
        {
            List<Pilot> pilotList = new List<Pilot>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                //Only want to show name, dob, email, and job title.
                string query = "SELECT * FROM dbo.Pilots";
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {



                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Pilot temp = new Pilot(reader["PilotName"].ToString(), reader["PilotEmail"].ToString());

                        temp.id = Convert.ToInt32(reader["PilotId"]);


                        pilotList.Add(temp);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get pilots!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }

                return pilotList;
            
            }
        }

        public Pilot GetPilot(int id)
        {
            Pilot pilot = new Pilot();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                string query = "SELECT * FROM dbo.Pilots WHERE PilotId = @id";

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
                        pilot = new Pilot(reader["PilotName"].ToString(), reader["PilotEmail"].ToString());

                        pilot.id = id;
                    }
                }catch(SqlException ex)
                {
                    Console.WriteLine("Could not get plane\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return pilot;
            }
        }

        public void AddPilot(Pilot pilot)
        {
            int id = 0;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                string query = @"INSERT INTO dbo.Pilots (PilotName, PilotEmail) values (@PilotName, @PilotEmail)";

                SqlTransaction transaction = conn.BeginTransaction("T1");
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.Transaction = transaction;
                cmd.Parameters.AddWithValue("@PilotName", pilot.name);
                cmd.Parameters.AddWithValue("@PilotEmail", pilot.email);

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
               


                    pilot.id = id;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not add plane!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }





            }
        }

        public void DeletePilot(int id)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                string query = "Delete from dbo.Pilots where PilotId = @Id";

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
                    Console.WriteLine("Could not delete pilot!!\n {0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }


            }
        }

        public void UpdatePilot(Pilot pilot)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                string query = $"update dbo.Pilots set PilotName = @PilotName, PilotEmail = @PilotEmail where PilotId = @id";

                SqlTransaction transaction = conn.BeginTransaction("T1");
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.Transaction = transaction;
                cmd.Parameters.AddWithValue("@Id", pilot.id);
                cmd.Parameters.AddWithValue("@PilotEmail", pilot.email);
                cmd.Parameters.AddWithValue("@PilotName", pilot.name);


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
                    Console.WriteLine("Could not update pilot!!\n {0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }


            }
        }
    }
}
