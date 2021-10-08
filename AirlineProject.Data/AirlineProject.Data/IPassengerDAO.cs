using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject.Data
{
    interface IPassengerDAO
    {
            //Need a method to get all passengers in database
            public IEnumerable<Passenger> GetPassengers();

            //Need a method to get specific passenger from database
            public Passenger GetPassenger(int id);

            //neead a method to add a passenger to database
            public void AddPassenger(Passenger passenger);
    }
}
