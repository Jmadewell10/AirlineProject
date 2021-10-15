using AirlineProject.Data;
using AirlineProject.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineProject.Web.Controllers
{
    public class PassengerController : Controller
    {
        private readonly IPassengerDAO passengerDAO;
        private readonly IFlightDAO flightDAO;

        public PassengerController(IPassengerDAO passengerdao)
        {
            this.passengerDAO = passengerdao;
        }

        // GET: PassengerController
        public IActionResult Index()
        {
            IEnumerable<Passenger> mpassengers = passengerDAO.GetPassengers();
            List<Passenger> model = new List<Passenger>();

            foreach(var passenger in mpassengers)
            {
                Passenger temp = new Passenger()
                {
                    id = passenger.id,
                    name = passenger.name,
                    email = passenger.email,
                    dob = passenger.dob,
                    jobTitle = passenger.jobTitle,
                    confirmationNumber = passenger.confirmationNumber
                    
                };

                model.Add(temp);

            }
            return View(model);
        }

        // GET: PassengerController/Details/5
        public ActionResult Details(int id)
        {
            Passenger model = passengerDAO.GetPassenger(id);
            return View(model);
        }
        [HttpGet]
        // GET: PassengerController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: PassengerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] Passenger passenger)
        {
            if(passenger.HowFull(passenger.flight.id) < passenger.CheckCapacity(passenger.flight.id))
            {
                if (ModelState.IsValid)
                {
                    Passenger newPassenger = new Passenger();
                    newPassenger.name = passenger.name;
                    newPassenger.id = passenger.id;
                    newPassenger.email = passenger.email;
                    newPassenger.dob = passenger.dob;
                    newPassenger.confirmationNumber = passenger.confirmationNumber;

                    passengerDAO.AddPassenger(passenger);

                    return RedirectToAction("Index");

                }
            }
            return View(passenger);
        }

        // GET: PassengerController/Edit/5
        public ActionResult Edit(int id)
        {
            Passenger model = passengerDAO.GetPassenger(id);
            return View(model);
        }

        // POST: PassengerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind]Passenger passenger)
        {
            if (ModelState.IsValid)
            {
                Passenger newPassenger = new Passenger();
                newPassenger.name = passenger.name;
                newPassenger.id = passenger.id;
                newPassenger.email = passenger.email;
                newPassenger.dob = passenger.dob;
                newPassenger.confirmationNumber = passenger.confirmationNumber;

                passengerDAO.UpdatePassenger(passenger);

                return RedirectToAction("Index");

            }
            return View(passenger);
        }

        // GET: PassengerController/Delete/5
        public ActionResult Delete(int id)
        {
            Passenger model = passengerDAO.GetPassenger(id);

            return View(model);
        }

        // POST: PassengerController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Passenger passenger = passengerDAO.GetPassenger(id);
                passengerDAO.DeletePassenger(passenger.id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
