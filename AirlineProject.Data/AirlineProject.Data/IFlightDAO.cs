using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject.Data
{
    public interface IFlightDAO
    {
        public IEnumerable<Flight> GetFights();

        public Flight GetFlight(int id);

        public void AddFlight(Flight flight);

        public void DeleteFlight(int id);

        public void UpdateFlight(Flight flight);
    }
}
