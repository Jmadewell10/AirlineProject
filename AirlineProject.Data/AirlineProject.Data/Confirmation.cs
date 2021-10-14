using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject.Data
{
    public class Confirmation
    {
        public int id { get; set; }

        public int flightId { get; set; }

        public Confirmation()
        {

        }

        public int GetFlight(int id)
        {
            ConfirmationDAO dao = new ConfirmationDAO();

            Confirmation confirmation = dao.GetConfirmation(id);

            int flightId = confirmation.flightId;

            return flightId;
        }

        public int HowFull(int id)
        {
            int filled = 0;

            ConfirmationDAO dao = new ConfirmationDAO();
            List<Confirmation> confirmations = new List<Confirmation>();
            
            foreach(var confirmation in dao.GetConfirmations())
            {
                confirmations.Add(confirmation);
            }

            foreach(var c in confirmations)
            {
                int count = 0;
                if(c.flightId == id)
                {
                    count++;
                }
                filled = count;
            }

            return filled;
        }
    }
}
