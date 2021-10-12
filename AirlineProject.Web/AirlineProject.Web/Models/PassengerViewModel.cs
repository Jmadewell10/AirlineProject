using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineProject.Web.Models
{
    public class PassengerViewModel
    {
        
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name: ")]
        [StringLength(50)]

        public string name { get; set; }

        [Required]
        [Display(Name = "Email: ")]
        [StringLength(50)]
        public string email { get; set; }

        [Required]
        [Display(Name = "Date of Birth: ")]
        [DataType(DataType.Date)]
        public string dob { get; set; }
        [Required]
        [Display(Name = "Job Title: ")]
        [StringLength(50)]
        public string jobTitle { get; set; }

        [Required]
        [Display(Name = "Confirmation Number: ")]
        public int confirmationNumber { get; set; }
    }
}
