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
    public class Tasks1Controller : Controller
    {
        private DB_Entities db = new DB_Entities();

        // GET: Tasks1
        public ActionResult Index()
        {
            var tasks = db.Tasks.Include(t => t.Activity).Include(t => t.Labor).Include(t => t.Location).Include(t => t.User);
            return View(tasks.ToList());
        }

        // GET: Tasks1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks1/Create
        public ActionResult Create()
        {
            ViewBag.Activity_Id = new SelectList(db.Activities, "Activity_Id", "Activity_name");
            ViewBag.Id_labor = new SelectList(db.Labors, "Id_labor", "Name");
            ViewBag.Id_location = new SelectList(db.Locations, "Id_location", "Name");
            ViewBag.UserId = new SelectList(db.User, "UserId", "Username");
            return View();
        }

        // POST: Tasks1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Task_Id,Name,Date,Number_hours,Hour_type,UserId,Activity_Id,Id_labor,Id_location")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Activity_Id = new SelectList(db.Activities, "Activity_Id", "Activity_name", task.Activity_Id);
            ViewBag.Id_labor = new SelectList(db.Labors, "Id_labor", "Name", task.Id_labor);
            ViewBag.Id_location = new SelectList(db.Locations, "Id_location", "Name", task.Id_location);
            ViewBag.UserId = new SelectList(db.User, "UserId", "Username", task.UserId);
            return View(task);
        }

        // GET: Tasks1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.Activity_Id = new SelectList(db.Activities, "Activity_Id", "Activity_name", task.Activity_Id);
            ViewBag.Id_labor = new SelectList(db.Labors, "Id_labor", "Name", task.Id_labor);
            ViewBag.Id_location = new SelectList(db.Locations, "Id_location", "Name", task.Id_location);
            ViewBag.UserId = new SelectList(db.User, "UserId", "Username", task.UserId);
            return View(task);
        }

        // POST: Tasks1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Task_Id,Name,Date,Number_hours,Hour_type,UserId,Activity_Id,Id_labor,Id_location")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Activity_Id = new SelectList(db.Activities, "Activity_Id", "Activity_name", task.Activity_Id);
            ViewBag.Id_labor = new SelectList(db.Labors, "Id_labor", "Name", task.Id_labor);
            ViewBag.Id_location = new SelectList(db.Locations, "Id_location", "Name", task.Id_location);
            ViewBag.UserId = new SelectList(db.User, "UserId", "Username", task.UserId);
            return View(task);
        }

        // GET: Tasks1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
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
