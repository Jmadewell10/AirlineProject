using AirlineProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineProject.Web.Controllers
{
    public class PlaneController : Controller
    {
        private readonly PlaneDAO planeDAO = new PlaneDAO();
        // GET: PlaneController
        public ActionResult Index()
        {

            IEnumerable<Plane> mplanes = planeDAO.GetPlanes();
            List<Plane> model = new List<Plane>();

            foreach (var plane in mplanes)
            {
                Plane temp = new Plane()
                {
                    id = plane.id,
                    name = plane.name,
                    capacity = plane.capacity

                };

                model.Add(temp);

            }
            return View(model);
        }

        // GET: PlaneController/Details/5
        public ActionResult Details(int id)
        {
            Plane plane = planeDAO.GetPlane(id);
            return View(plane);
        }

        // GET: PlaneController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlaneController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] Plane plane)
        {
            if (ModelState.IsValid)
            {
                Plane newPlane = new Plane();
                newPlane.id = plane.id;
                newPlane.name = plane.name;
                newPlane.capacity = plane.capacity;


                planeDAO.AddPlane(newPlane);

                return RedirectToAction("Index");
            }

            return View(plane);
        }

        // GET: PlaneController/Edit/5
        public ActionResult Edit(int id)
        {
            Plane plane = planeDAO.GetPlane(id);
            return View(plane);
        }

        // POST: PlaneController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind] Plane plane)
        {
            if (ModelState.IsValid)
            {
                Plane newPlane = new Plane();

                newPlane.id = plane.id;
                newPlane.name = plane.name;
                newPlane.capacity = plane.capacity;


                planeDAO.UpdatePlane(newPlane);

                return RedirectToAction("Index");
            }

            return View(plane);
        }

        // GET: PlaneController/Delete/5
        public ActionResult Delete(int id)
        {
            Plane plane = planeDAO.GetPlane(id);
            return View(plane);
        }

        // POST: PlaneController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Plane plane = planeDAO.GetPlane(id);
                planeDAO.DeletePlane(plane.id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
