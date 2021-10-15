using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject.Data
{
    public class Pilot
    {
        [Display(Name ="Pilot Id: ")]
        public int id { get; set; }

        [Display(Name ="Name: ")]
        public string name { get; set; }
        [Display(Name ="Email: ")]
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
