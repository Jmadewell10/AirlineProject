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
        [Display(Name = "Plane Id: ")]
        public int id{ get; set; }
        [Display (Name = "Plane: ")]
        public string name { get; set; }
        [Display(Name = "Capacity: ")]
        public int capacity { get; set; }
        


        public Plane()
        {

        }

        public Plane(string name, int capacity)
        {
            this.name = name;
            this.capacity = capacity;
            
        }

    }
}
