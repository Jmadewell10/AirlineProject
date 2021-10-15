using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject.Data
{
    public class FlightDAO : IFlightDAO
    {
        private string connString = "Data Source=JONATHAN-PC;Initial Catalog=Airline;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public IEnumerable<Flight> GetFights()
        {
          
            List<Flight> flightList = new List<Flight>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                //Only want to show name, dob, email, and job title.
                string query = "SELECT * FROM dbo.Flights INNER JOIN dbo.Planes on Flights.Plane = PlaneId Inner Join dbo.Pilots on Flights.Pilot=PilotId;";
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {



                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Plane tempPlane = new Plane(reader["PlaneName"].ToString(), Int32.Parse(reader["PlaneCapacity"].ToString()));
                        tempPlane.id = Convert.ToInt32(reader["PlaneId"]);

                        Pilot tempPilot = new Pilot(reader["PilotName"].ToString(), reader["PilotEmail"].ToString());
                        tempPilot.id = Convert.ToInt32(reader["PilotId"]);

                        Flight temp = new Flight(tempPlane, tempPilot, reader["ArrivalTime"].ToString(), reader["DepartureTime"].ToString(), reader["DepartureAirport"].ToString(), reader["ArrivalAirport"].ToString());

                        temp.id = Convert.ToInt32(reader["FlightId"]);


                        flightList.Add(temp);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get flights!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }

                return flightList;
            }

        }

        public Flight GetFlight(int id)
        {
            Flight flight = new Flight();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                string query = "Select * FROM dbo.Flights INNER JOIN dbo.Planes on Flights.Plane = PlaneId INNER JOIN dbo.Pilots on Flights.Pilot = PilotId  WHERE FlightId = @Id";

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

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Plane tempPlane = new Plane(reader["PlaneName"].ToString(), Int32.Parse(reader["PlaneCapacity"].ToString()));
                        tempPlane.id = Convert.ToInt32(reader["PlaneId"]);

                        Pilot tempPilot = new Pilot(reader["PilotName"].ToString(), reader["PilotEmail"].ToString());
                        tempPilot.id = Convert.ToInt32(reader["PilotId"]);

                        flight = new Flight(tempPlane, tempPilot, reader["ArrivalTime"].ToString(), reader["DepartureTime"].ToString(), reader["DepartureAirport"].ToString(), reader["ArrivalAirport"].ToString());

                        flight.id = id;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get flight\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return flight;
            }
        }

        public void AddFlight(Flight flight)
        {
            int id = 0;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                string query = @"INSERT INTO dbo.Flights (Plane, Pilot, ArrivalTime, DepartureTime, DepartureAirport, ArrivalAirport) values (@PlaneId, @PilotId, @ArrivalTime, @DepartureTime, @DepartureAirport, @ArrivalAirport)";

                SqlTransaction transaction = conn.BeginTransaction("T1");
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.Transaction = transaction;
                cmd.Parameters.AddWithValue("@PlaneId", flight.plane.id);
                cmd.Parameters.AddWithValue("@PilotId", flight.pilot.id);
                cmd.Parameters.AddWithValue("@ArrivalTime", flight.arrivalTime);
                cmd.Parameters.AddWithValue("@DepartureTime", flight.departureTime);
                cmd.Parameters.AddWithValue("@DepartureAirport", flight.departureAirport);
                cmd.Parameters.AddWithValue("@ArrivalAirport", flight.arrivalAirport);


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
                    Console.WriteLine("Could not add flight!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void DeleteFlight(int id)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                string query = "Delete from dbo.Flights where FlightId = @Id";

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
                    Console.WriteLine("Could not delete flight!!\n {0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }


            }
        }

        public void UpdateFlight(Flight flight)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                string query = $"update dbo.Flights set Plane = @PlaneId, Pilot = @PilotId, ArrivalAirport = @AAirport, DepartureTime = @DTime, ArrivalTime = @ATime, DepartureAirport = @DAirport WHERE FlightId = @Id";

                SqlTransaction transaction = conn.BeginTransaction("T1");
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.Transaction = transaction;
                cmd.Parameters.AddWithValue("@Id", flight.id);
                cmd.Parameters.AddWithValue("@PlaneId", flight.plane.id);
                cmd.Parameters.AddWithValue("@PilotId", flight.pilot.id);
                cmd.Parameters.AddWithValue("@AAirport", flight.arrivalAirport);
                cmd.Parameters.AddWithValue("@DAirport", flight.departureAirport);
                cmd.Parameters.AddWithValue("@ATime", flight.arrivalTime);
                cmd.Parameters.AddWithValue("@DTime", flight.departureTime);



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
                    Console.WriteLine("Could not update flight!!\n {0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }


            }
        }


    }
}
