using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject.Data
{
    public class Passenger
    {
        [Display(Name = "Passenger Id: ")]
        public int id { get; set; }

        [Display(Name = "Name: ")]
        public string name { get; set; }

        [Display(Name = "Date Of Birth: ")]
        public string dob { get; set; }

        [Display(Name = "Email: ")]
        public string email { get; set; }

        [Display(Name = "Job Title: ")]
        public string jobTitle { get; set; }
        [Display(Name = "Confirmation Number: ")]
        public string confirmationNumber { get; set; }
        [Display(Name ="Flight Information: ")]
        public Flight flight { get; set; }

        public Passenger()
        {

        }

        public Passenger(string name, string dob, string email, string jobTitle, Flight flight)
        {
            this.name = name;
            this.dob = dob;
            this.email = email;
            this.jobTitle = jobTitle;
            this.flight = flight;
        }

        public override string ToString()
        {
            return $"[Passenger Id: {id}, Name: {name}, Email: {email}, Job Title: {jobTitle}, Confirmation Number {confirmationNumber}]";
        }
        public int CheckCapacity(int id)
        {
            int capacity = 0;

            FlightDAO dao = new FlightDAO();
            PlaneDAO planeDao = new PlaneDAO();

           
            Flight flight = dao.GetFlight(id);
            Plane plane = planeDao.GetPlane(flight.id);
            flight.plane = plane;


            capacity = flight.plane.capacity;

            return capacity;
        }

        public int HowFull(int id)
        {
            int filled = 0;

            FlightDAO flightDAO = new FlightDAO();

            Flight flight = flightDAO.GetFlight(id);

            List<Flight> flights = new List<Flight>();

            foreach (var flight1 in flightDAO.GetFights())
            {
                flights.Add(flight1);
            }

            foreach (var f in flights)
            {
                int count = 0;
                if (f.id == id)
                {
                    count++;
                    filled = count;
                }

            }

            return filled;
        }

        public int GetId(Passenger passenger)
        {
            PassengerDAO passengerDAO = new PassengerDAO();
            Passenger newPassenger = passengerDAO.GetPassengerByName(passenger.name);

            return newPassenger.id;

        }
    }
}
