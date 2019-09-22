using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CopeyWinery.Models;

namespace CopeyWinery.Controllers
{
    public class LocationsController : Controller
    {
        private DB_Entities db = new DB_Entities();

        // GET: Locations
        public ActionResult Index(bool? deleted, bool? added, bool? updated)
        {
            var model = db.Locations.ToList();
            if (deleted != null || added != null || updated != null)
                {
                if (deleted == true)
                {
                    ModelState.AddModelError("", "Ubicacion eliminada satisfactoriamente");
                }
                else
                {
                    if (added == true)
                    {
                        ModelState.AddModelError("", "Ubicacion agregada satisfactoriamente");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Ubicacion editada satisfactoriamente");

                    }
                }
            }
            return View(model);
        }

        // GET: Locations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] Location location)
        {
            if (location.Name == null)
            {
                ModelState.AddModelError("", "Debe indicar un nombre para la ubicacion");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Locations.Add(location);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { added = true });
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Algo salio mal, intente nuevamente");

                    return View(location);
                }
            }
            return View(location);
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_location,Name")] Location location)
        {
            if (location.Name == null)
            {
                ModelState.AddModelError("", "Debe indicar un nombre para la ubicacion");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(location).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { updated = true });
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Algo salio mal, intente nuevamente");

                    return View(location);
                }
            }
            return View(location);

           


        }

        // GET: Locations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Location location = db.Locations.Find(id);
            
            try
            {
                db.Locations.Remove(location);
                db.SaveChanges();
                return RedirectToAction("Index", new { deleted = true });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "No es posible eliminar esta ubicacion, compruebe que no este ligada a ninguna tarea registrada");

                return View(location);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
