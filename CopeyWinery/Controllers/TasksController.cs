﻿using System;
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
    public class TasksController : Controller
    {
        private DB_Entities db = new DB_Entities();

        // GET: Tasks
        public ActionResult Index()
        {
            var tasks = db.Tasks.Include(t => t.Activity1).Include(t => t.Labor1).Include(t => t.Lane1).Include(t => t.Location1).Include(t => t.User);
            return View(tasks.ToList());
        }

        // GET: Tasks/Details/5
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

        // GET: Tasks/Create
        public ActionResult Create()
        {
            ViewBag.Activity = new SelectList(db.Activities, "Activity_Id", "Activity_name");
            ViewBag.Labor = new SelectList(db.Labors, "Id_labor", "Name");
            ViewBag.Lane = new SelectList(db.Lanes, "Id_lane", "Name");
            ViewBag.Location = new SelectList(db.Locations, "Id_location", "Name");
            ViewBag.Users = new SelectList(db.User, "UserId", "Username");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Task_Id,Name,Date,Number_hours,Hour_type,Quantity,Unit,Users,Activity,Labor,Location,Lane")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Activity = new SelectList(db.Activities, "Activity_Id", "Activity_name", task.Activity);
            ViewBag.Labor = new SelectList(db.Labors, "Id_labor", "Name", task.Labor);
            ViewBag.Lane = new SelectList(db.Lanes, "Id_lane", "Name", task.Lane);
            ViewBag.Location = new SelectList(db.Locations, "Id_location", "Name", task.Location);
            ViewBag.Users = new SelectList(db.User, "UserId", "Username", task.Users);
            return View(task);
        }

        // GET: Tasks/Edit/5
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
            ViewBag.Activity = new SelectList(db.Activities, "Activity_Id", "Activity_name", task.Activity);
            ViewBag.Labor = new SelectList(db.Labors, "Id_labor", "Name", task.Labor);
            ViewBag.Lane = new SelectList(db.Lanes, "Id_lane", "Name", task.Lane);
            ViewBag.Location = new SelectList(db.Locations, "Id_location", "Name", task.Location);
            ViewBag.Users = new SelectList(db.User, "UserId", "Username", task.Users);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Task_Id,Name,Date,Number_hours,Hour_type,Quantity,Unit,Users,Activity,Labor,Location,Lane")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Activity = new SelectList(db.Activities, "Activity_Id", "Activity_name", task.Activity);
            ViewBag.Labor = new SelectList(db.Labors, "Id_labor", "Name", task.Labor);
            ViewBag.Lane = new SelectList(db.Lanes, "Id_lane", "Name", task.Lane);
            ViewBag.Location = new SelectList(db.Locations, "Id_location", "Name", task.Location);
            ViewBag.Users = new SelectList(db.User, "UserId", "Username", task.Users);
            return View(task);
        }

        // GET: Tasks/Delete/5
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

        // POST: Tasks/Delete/5
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
