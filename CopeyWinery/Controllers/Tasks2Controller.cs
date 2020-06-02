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
    public class Tasks2Controller : Controller
    {
        private DB_Entities db = new DB_Entities();

        public ActionResult Index(DateTime? start, DateTime? end)
        {
            ViewBag.start = start;
            ViewBag.end = end;
            var tasks = db.Tasks
                        .Include(t => t.Activity)
                        .Include(t => t.Location)
                        .Include(t => t.User)
                        .Include(t => t.Labor)
                        .Where(x=> x.Date >= start
                        && x.Date <= end)
                        .OrderByDescending(t => t.Date); 

            return View(tasks);
        }


        // GET: Tasks2
        //public ActionResult Index()
        //{
        //    var tasks = db.Tasks.Include(t => t.Activity).Include(t => t.Location).Include(t => t.User).Include(t => t.Labor);
        //    return View(tasks.ToList());
        //}

        // GET: Tasks2/Details/5
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

        // GET: Tasks2/Create
        public ActionResult Create()
        {
            ViewBag.Activity_Id = new SelectList(db.Activities, "Activity_Id", "Activity_name");
            ViewBag.Id_location = new SelectList(db.Locations, "Id_location", "Name");
            ViewBag.UserId = new SelectList(db.User, "UserId", "Username");
            ViewBag.Id_labor = new SelectList(db.Labors, "Id_labor", "Name");
            return View();
        }

        // POST: Tasks2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Task_Id,Name,Date,Number_hours,Hour_type,UserId,Activity_Id,Id_labor,Id_location,Ext_Attr_Labor_Value")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Activity_Id = new SelectList(db.Activities, "Activity_Id", "Activity_name", task.Activity_Id);
            ViewBag.Id_location = new SelectList(db.Locations, "Id_location", "Name", task.Id_location);
            ViewBag.UserId = new SelectList(db.User, "UserId", "Username", task.UserId);
            ViewBag.Id_labor = new SelectList(db.Labors, "Id_labor", "Name", task.Id_labor);
            return View(task);
        }

        // GET: Tasks2/Edit/5
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
            ViewBag.Id_location = new SelectList(db.Locations, "Id_location", "Name", task.Id_location);
            ViewBag.UserId = new SelectList(db.User, "UserId", "Username", task.UserId);
            ViewBag.Id_labor = new SelectList(db.Labors, "Id_labor", "Name", task.Id_labor);
            return View(task);
        }

        // POST: Tasks2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Task_Id,Name,Date,Number_hours,Hour_type,UserId,Activity_Id,Id_labor,Id_location,Ext_Attr_Labor_Value")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Activity_Id = new SelectList(db.Activities, "Activity_Id", "Activity_name", task.Activity_Id);
            ViewBag.Id_location = new SelectList(db.Locations, "Id_location", "Name", task.Id_location);
            ViewBag.UserId = new SelectList(db.User, "UserId", "Username", task.UserId);
            ViewBag.Id_labor = new SelectList(db.Labors, "Id_labor", "Name", task.Id_labor);
            return View(task);
        }

        // GET: Tasks2/Delete/5
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

        // POST: Tasks2/Delete/5
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
