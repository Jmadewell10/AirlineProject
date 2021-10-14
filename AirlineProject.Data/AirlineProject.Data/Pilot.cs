using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject.Data
{
    public class Pilot
    {
        public int id { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public Pilot()
        {

        }

        public Pilot(string name, string email)
        {
            this.name = name;
            this.email = email;
        }

    }
}
