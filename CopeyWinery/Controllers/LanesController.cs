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
    public class LanesController : Controller
    {
        private DB_Entities db = new DB_Entities();

        // GET: Lanes
        public ActionResult Index()
        {
            return View(db.Lanes.ToList());
        }

        // GET: Lanes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lane lane = db.Lanes.Find(id);
            if (lane == null)
            {
                return HttpNotFound();
            }
            return View(lane);
        }

        // GET: Lanes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lanes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_lane,Name,plant_quantity")] Lane lane)
        {
            if (ModelState.IsValid)
            {
                db.Lanes.Add(lane);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lane);
        }

        // GET: Lanes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lane lane = db.Lanes.Find(id);
            if (lane == null)
            {
                return HttpNotFound();
            }
            return View(lane);
        }

        // POST: Lanes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_lane,Name,plant_quantity")] Lane lane)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lane).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lane);
        }

        // GET: Lanes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lane lane = db.Lanes.Find(id);
            if (lane == null)
            {
                return HttpNotFound();
            }
            return View(lane);
        }

        // POST: Lanes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lane lane = db.Lanes.Find(id);
            db.Lanes.Remove(lane);
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
