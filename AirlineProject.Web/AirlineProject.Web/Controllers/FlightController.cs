using AirlineProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineProject.Web.Controllers
{
    public class FlightController : Controller
    {
        private readonly FlightDAO flightDAO = new FlightDAO();
        // GET: FlightController
        public ActionResult Index()
        {
            
            IEnumerable<Flight> mflights = flightDAO.GetFights();
            List<Flight> model = new List<Flight>();

            foreach (var flight in mflights)
            {
                Flight temp = new Flight()
                {
                    id = flight.id,
                    departureAirport = flight.departureAirport,
                    arrivalAirport = flight.arrivalAirport,
                    departureTime = flight.departureTime,
                    arrivalTime = flight.arrivalTime,
                    pilot = flight.pilot,
                    plane = flight.plane
                    

                };

                model.Add(temp);

            }
            return View(model);
        }

        // GET: FlightController/Details/5
        public ActionResult Details(int id)
        {
            Flight flight = flightDAO.GetFlight(id);
            return View(flight);
        }

        // GET: FlightController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FlightController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] Flight flight)
        {
            if (ModelState.IsValid)
            {
                Flight newFlight = new Flight();
                newFlight.id = flight.id;
                newFlight.departureAirport = flight.departureAirport;
                newFlight.arrivalAirport = flight.arrivalAirport;
                newFlight.departureTime = flight.departureTime;
                newFlight.arrivalTime = flight.arrivalTime;
                newFlight.plane = flight.plane;
                newFlight.pilot = flight.pilot;

                flightDAO.AddFlight(newFlight);

                return RedirectToAction("Index");
            }
            return View(flight);
        }

        // GET: FlightController/Edit/5
        public ActionResult Edit(int id)
        {
            Flight flight = flightDAO.GetFlight(id);
            return View(flight);
        }

        // POST: FlightController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind] Flight flight)
        {
            if (ModelState.IsValid)
            {
                Flight newFlight = new Flight();
                newFlight.id = flight.id;
                newFlight.departureAirport = flight.departureAirport;
                newFlight.arrivalAirport = flight.arrivalAirport;
                newFlight.departureTime = flight.departureTime;
                newFlight.arrivalTime = flight.arrivalTime;
                newFlight.plane = flight.plane;
                newFlight.pilot = flight.pilot;

                flightDAO.UpdateFlight(newFlight);

                return RedirectToAction("Index");
            }
            return View(flight);
        }

        // GET: FlightController/Delete/5
        public ActionResult Delete(int id)
        {
            Flight flight = flightDAO.GetFlight(id);
            return View(flight);
        }

        // POST: FlightController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Flight flight = flightDAO.GetFlight(id);
                flightDAO.DeleteFlight(flight.id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
