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

        public PassengerController(IPassengerDAO passengerdao)
        {
            this.passengerDAO = passengerdao;
        }

        // GET: PassengerController
        public IActionResult Index()
        {
            IEnumerable<Passenger> mpassengers = passengerDAO.GetPassengers();
            List<PassengerViewModel> model = new List<PassengerViewModel>();

            foreach(var passenger in mpassengers)
            {
                PassengerViewModel temp = new PassengerViewModel()
                {
                    Id = passenger.id,
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
        public ActionResult Create([Bind] PassengerViewModel passenger)
        {
            if (ModelState.IsValid)
            {
                Passenger newPassenger = new Passenger();
                newPassenger.name = passenger.name;
                newPassenger.id = passenger.Id;
                newPassenger.email = passenger.email;
                newPassenger.dob = passenger.dob;
                newPassenger.confirmationNumber = passenger.confirmationNumber;

                passengerDAO.AddPassenger(newPassenger);

                return RedirectToAction("Index");
            }

            return View(passenger);
        }

        // GET: PassengerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PassengerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PassengerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PassengerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
