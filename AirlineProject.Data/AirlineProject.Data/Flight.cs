using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject.Data
{
    public class Flight
    {
        [Display(Name ="Flight Number: ")]
        public int id { get; set; }

        [Display(Name ="Plane: ")]
        public Plane plane { get; set; }

        [Display(Name = "Pilot: ")]
        public Pilot pilot { get; set; }

        [Display(Name = "Arrival Time")]
        public string arrivalTime { get; set; }
        [Display(Name ="Departure Time")]
        public string departureTime { get; set; }
        [Display(Name ="Departs From: ")]
        public string departureAirport { get; set; }
        [Display(Name ="Arrives At: ")]
        public string arrivalAirport { get; set; }

        public Flight()
        {

        }

        public Flight(Plane plane, Pilot pilot, string arrivalTime, string departureTime, string departureAirport, string arrivalAirport)
        {
            this.plane = plane;
            this.pilot = pilot;
            this.arrivalTime = arrivalTime;
            this.departureTime = departureTime;
            this.departureAirport = departureAirport;
            this.arrivalAirport = arrivalAirport;
        }

    }
}
