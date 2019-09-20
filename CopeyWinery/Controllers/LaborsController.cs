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
    public class LaborsController : Controller
    {
        private DB_Entities db = new DB_Entities();

        // GET: Labors
        public ActionResult Index()
        {
            return View(db.Labors.ToList());
        }

        // GET: Labors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Labor labor = db.Labors.Find(id);
            if (labor == null)
            {
                return HttpNotFound();
            }
            return View(labor);
        }

        // GET: Labors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Labors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] Labor labor)
        {
            if (labor.Name == null)
            {
                ModelState.AddModelError("", "Debe seleccionar un nombre para la labor");
            }

            if (ModelState.IsValid)
            {
                db.Labors.Add(labor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(labor);
        }

        // GET: Labors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Labor labor = db.Labors.Find(id);
            if (labor == null)
            {
                return HttpNotFound();
            }
            return View(labor);
        }

        // POST: Labors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_labor,Name")] Labor labor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(labor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(labor);
        }

        // GET: Labors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Labor labor = db.Labors.Find(id);
            if (labor == null)
            {
                return HttpNotFound();
            }
            return View(labor);
        }

        // POST: Labors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Labor labor = db.Labors.Find(id);
            db.Labors.Remove(labor);
            db.SaveChanges();
            return RedirectToAction("Index");
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
