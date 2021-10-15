using AirlineProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineProject.Web.Controllers
{
    public class PilotController : Controller
    {
        private readonly PilotDAO pilotDAO = new PilotDAO();
        // GET: PilotController
        public ActionResult Index()
        {
            IEnumerable<Pilot> mpilots = pilotDAO.GetPilots();
            List<Pilot> model = new List<Pilot>();

            foreach (var pilot in mpilots)
            {
                Pilot temp = new Pilot()
                {
                    id = pilot.id,
                    name = pilot.name,
                    email = pilot.email
                };

                model.Add(temp);

            }
            return View(model);
        }

        // GET: PilotController/Details/5
        public ActionResult Details(int id)
        {
            Pilot model = pilotDAO.GetPilot(id);
            return View(model);
        }

        // GET: PilotController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: PilotController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] Pilot pilot)
        {
            if (ModelState.IsValid)
            {
                Pilot newPilot = new Pilot();
                newPilot.id = pilot.id;
                newPilot.name = pilot.name;
                newPilot.email = pilot.email;

                
                pilotDAO.AddPilot(newPilot);
                
                
                return RedirectToAction("Index");
            }

            return View(pilot);
        }

        // GET: PilotController/Edit/5
        public ActionResult Edit(int id)
        {
            Pilot model = pilotDAO.GetPilot(id);
            return View(model);
        }

        // POST: PilotController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind] Pilot pilot)
        {
            if (ModelState.IsValid)
            {
                Pilot newPilot = new Pilot();
                newPilot.id = pilot.id;
                newPilot.name = pilot.name;
                newPilot.email = pilot.email;

                pilotDAO.UpdatePilot(newPilot);
                return RedirectToAction("Index");
            }
            return View(pilot);
        }

        // GET: PilotController/Delete/5
        public ActionResult Delete(int id)
        {
            Pilot pilot = pilotDAO.GetPilot(id);
            return View(pilot);
        }

        // POST: PilotController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Pilot pilot = pilotDAO.GetPilot(id);
                pilotDAO.DeletePilot(pilot.id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
