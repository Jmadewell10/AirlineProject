using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject.Data
{
    public interface IPilotDAO
    {
        public IEnumerable<Pilot> GetPilots();

        public Pilot GetPilot(int id);

        public void AddPilot(Pilot pilot);

        public void DeletePilot(int id);

        public void UpdatePilot(Pilot pilot);
    }
}
