using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject.Data
{
    public class Plane
    {
        public int id{ get; set; }
        [Display (Name = "Name")]
        public string name { get; set; }
        [Display(Name = "Capacity")]
        public int capacity { get; set; }
        [Display(Name = "Number Of Pilots")]
        public int numberOfPilots { get; set; }


        public Plane()
        {

        }

        public Plane(string name, int capacity, int numberOfPilots)
        {
            this.name = name;
            this.capacity = capacity;
            this.numberOfPilots = numberOfPilots;
        }

    }
}
