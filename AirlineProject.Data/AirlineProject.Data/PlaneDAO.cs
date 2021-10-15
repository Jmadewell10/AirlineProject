using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject.Data
{
    public class PlaneDAO : IPlaneDAO
    {
        private string connString = "Data Source=JONATHAN-PC;Initial Catalog=Airline;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public IEnumerable<Plane> GetPlanes()
        {
            List<Plane> planeList = new List<Plane>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                //Only want to show name, dob, email, and job title.
                string query = "SELECT * FROM dbo.Planes";
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {



                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Plane temp = new Plane(reader["PlaneName"].ToString(), Int32.Parse(reader["PlaneCapacity"].ToString()));

                        temp.id = Convert.ToInt32(reader["PlaneId"]);
                        

                        planeList.Add(temp);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get planes!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }

                return planeList;
            }
        }
    

        public Plane GetPlane(int id)
        {
            Plane plane = new Plane();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                string query = "SELECT * FROM dbo.Planes WHERE PlaneId = @id";

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
                        plane = new Plane(reader["PlaneName"].ToString(), Int32.Parse(reader["PlaneCapacity"].ToString()));

                        plane.id = id;
                    }
                }catch(SqlException ex)
                {
                    Console.WriteLine("Could not get plane\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return plane;
            }
        }

        public void AddPlane(Plane plane)
        {
            int id = 0;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                string query = @"INSERT INTO dbo.Planes (PlaneName, PlaneCapacity) values (@PlaneName, @PlaneCapacity)";

                SqlTransaction transaction = conn.BeginTransaction("T1");
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.Transaction = transaction;
                cmd.Parameters.AddWithValue("@PlaneName", plane.name);
                cmd.Parameters.AddWithValue("@PlaneCapacity", plane.capacity);


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
                    Console.WriteLine("Could not add plane!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }





            }
        }

        public void DeletePlane(int id)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                string query = "Delete from dbo.Planes where PlaneId = @Id";

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
                    Console.WriteLine("Could not delete plane!!\n {0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }


            }
        }

        public void UpdatePlane(Plane plane)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                string query = $"update dbo.Planes set PlaneName = @PlaneName, PlaneCapacity = @PlaneCapacity where PlaneId = @id";

                SqlTransaction transaction = conn.BeginTransaction("T1");
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.Transaction = transaction;
                cmd.Parameters.AddWithValue("@Id", plane.id);
                cmd.Parameters.AddWithValue("@PlaneCapacity", plane.capacity);


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
                    Console.WriteLine("Could not update plane!!\n {0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }


            }
        }
    }
}


