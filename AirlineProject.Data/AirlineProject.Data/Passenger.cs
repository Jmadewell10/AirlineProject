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
        public int confirmationNumber { get; set; }

        public Passenger()
        {

        }

        public Passenger(string name, string dob, string email, string jobTitle)
        {
            this.name = name;
            this.dob = dob;
            this.email = email;
            this.jobTitle = jobTitle;
        }

        public override string ToString()
        {
            return $"[Passenger Id: {id}, Name: {name}, Email: {email}, Job Title: {jobTitle}, Confirmation Number {confirmationNumber}]";
        }
    }
}
