using System;
using System.Collections;
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
    public class TaskObject
    {
        public DateTime? Date { get; set; }
        public int? Number_hours { get; set; }
        public string Hour_type { get; set; }
        public int? Activity { get; set; }
        public int? Labor { get; set; }
        public int? Location { get; set; }
        public int? User { get; set; }


    }
    public class TasksController : Controller
    {
        private DB_Entities db = new DB_Entities();

        // GET: Tasks
        [Authorize]
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");

            }
            if (!User.IsInRole("Administrator"))
            {
                 var tasks = db.Tasks.Include(t => t.Activity1).Include(t => t.Labor1).Include(t => t.Lane1).Include(t => t.Location1).Include(t => t.User1).Where(x=> x.User1.Username== User.Identity.Name);
                return View(tasks.ToList());

            }
            else {
                 var tasks = db.Tasks.Include(t => t.Activity1).Include(t => t.Labor1).Include(t => t.Lane1).Include(t => t.Location1).Include(t => t.User1);
                return View(tasks.ToList());

            }
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

        public ActionResult Create()
        {
            return View(new Task());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(Task model)
        {
            if (model.Date == null)
            {
                ModelState.AddModelError("", "Debe ingresar un valor para la fecha!!!");
            }
            if (ModelState.IsValid)
            {
                TaskObject taskObject = new TaskObject();
                taskObject.Date = model.Date;
                return RedirectToAction("Add_Num_Hours", taskObject);
            }
            return View(model);
        }

        public ActionResult Add_Num_Hours(TaskObject taskObj)
        {
            Task task = new Task();
            task.Date = taskObj.Date;
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Add_Num_Hours(Task model)
        {
            if (model.Number_hours == null)
            {
                ModelState.AddModelError("", "Debe ingresar el numero de horas!!!");
            }
            if (ModelState.IsValid)
            {
                TaskObject taskObject = new TaskObject();
                taskObject.Date = model.Date;
                taskObject.Number_hours = model.Number_hours;
                return RedirectToAction("Add_Hour_type", taskObject);
            }
            return View(model);
        }

        public ActionResult Add_Hour_type(TaskObject taskObj)
        {
            Task task = new Task();
            task.Date = taskObj.Date;
            task.Number_hours = taskObj.Number_hours;
            List<string> hourTypeOption = new List<string>();
            hourTypeOption.Add("Ordinaria");
            hourTypeOption.Add("Extraordinaria");
            ViewBag.Hour_type = new SelectList(hourTypeOption);
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Hour_type(Task model)
        {
            if (model.Hour_type == null)
            {
                ModelState.AddModelError("", "Debe ingresar el tipo de hora!!!");
            }
            if (ModelState.IsValid)
            {
                TaskObject taskObject = new TaskObject();
                taskObject.Date = model.Date;
                taskObject.Number_hours = model.Number_hours;
                taskObject.Hour_type= model.Hour_type;
                return RedirectToAction("Add_Activity", taskObject);
            }
            return View(model);
        }


        public ActionResult Add_Activity(TaskObject taskObj)
        {
            Task task = new Task();
            task.Date = taskObj.Date;
            task.Number_hours = taskObj.Number_hours;
            task.Hour_type = taskObj.Hour_type;
            ViewBag.Activity = new SelectList(db.Activities, "Activity_Id", "Activity_name");
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Activity(Task model)
        {

            if (model.Activity == null)
            {
                ModelState.AddModelError("", "Debe seleccionar una actividad!!!");
            }
            if (ModelState.IsValid)
            {
              
                TaskObject taskObject = new TaskObject();
                taskObject.Date = model.Date;
                taskObject.Number_hours = model.Number_hours;
                taskObject.Hour_type = model.Hour_type;
                taskObject.Activity = model.Activity;
                return RedirectToAction("Add_Labor", taskObject);                
            }
            return View(model);
        }

        public ActionResult Add_Labor(TaskObject taskObj)
        {
            Task task = new Task();
            task.Date = taskObj.Date;
            task.Number_hours = taskObj.Number_hours;
            task.Hour_type = taskObj.Hour_type;
            task.Activity = taskObj.Activity;
            ViewBag.Labor = new SelectList(db.Labors, "Id_labor", "Name");
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Labor(Task model)
        {

            if (model.Labor == null)
            {
                ModelState.AddModelError("", "Debe seleccionar una labor!!!");
            }
            if (ModelState.IsValid)
            {

                TaskObject taskObject = new TaskObject();
                taskObject.Date = model.Date;
                taskObject.Number_hours = model.Number_hours;
                taskObject.Hour_type = model.Hour_type;
                taskObject.Activity = model.Activity;
                taskObject.Labor = model.Labor;
                return RedirectToAction("Add_Location", taskObject);
            }
            return View(model);
        }

        public ActionResult Add_Location(TaskObject taskObj)
        {
            Task task = new Task();
            task.Date = taskObj.Date;
            task.Number_hours = taskObj.Number_hours;
            task.Hour_type = taskObj.Hour_type;
            task.Activity = taskObj.Activity;
            task.Labor = taskObj.Labor;
            ViewBag.Location = new SelectList(db.Locations, "Id_location", "Name");
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Location(Task model)
        {

            if (model.Location == null)
            {
                ModelState.AddModelError("", "Debe seleccionar una ubicacion");
            }
            if (ModelState.IsValid)
            {

                TaskObject taskObject = new TaskObject();
                taskObject.Date = model.Date;
                taskObject.Number_hours = model.Number_hours;
                taskObject.Hour_type = model.Hour_type;
                taskObject.Activity = model.Activity;
                taskObject.Labor = model.Labor;
                taskObject.Location = model.Location;
                return RedirectToAction("CreateTask", taskObject);
            }
            return View(model);
        }

        // GET: Tasks/CreateNumber_hours
        public ActionResult CreateTask(TaskObject taskObj)
        {
            Task task = new Task();
            task.Date = taskObj.Date;
            task.Number_hours = taskObj.Number_hours;
            task.Hour_type = taskObj.Hour_type;
            task.Activity = taskObj.Activity;
            task.Labor = taskObj.Labor;
            task.Location = taskObj.Location;
            task.User = db.Users.Where(x => x.Username == User.Identity.Name).Select(x => x.UserId).FirstOrDefault();
            task.Name = task.Task_Id.ToString();
            db.Tasks.Add(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ExportView()
        {
            Response.AddHeader("content-disposition", "attachment;filename=Report1.xls");
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            return View(db.Tasks.AsEnumerable());
        }


        //// POST: Tasks/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Createbck([Bind(
        //    //Include = "Task_Id,Name,Date,Number_hours,Hour_type,Quantity,Unit,Users,Activity,Labor,Location,Lane")] Task task)
        //    Include = "Hour_type")] Task task)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Tasks.Add(task);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Activity = new SelectList(db.Activities, "Activity_Id", "Activity_name", task.Activity);
        //    ViewBag.Labor = new SelectList(db.Labors, "Id_labor", "Name", task.Labor);
        //    ViewBag.Lane = new SelectList(db.Lanes, "Id_lane", "Name", task.Lane);
        //    ViewBag.Location = new SelectList(db.Locations, "Id_location", "Name", task.Location);
        //    ViewBag.Users = new SelectList(db.User, "UserId", "Username", task.Users);
        //    return View(task);
        //}

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
            List<string> hourTypeOption = new List<string>();
            if (task.Hour_type == "Ordinaria")
            {
                hourTypeOption.Add("Ordinaria");
                hourTypeOption.Add("Extraordinaria");
            }
            else {
                hourTypeOption.Add("Extraordinaria");
                hourTypeOption.Add("Ordinaria");
            }

            
            ViewBag.Hour_type = new SelectList(hourTypeOption);
            ViewBag.Activity = new SelectList(db.Activities, "Activity_Id", "Activity_name", task.Activity);
            ViewBag.Labor = new SelectList(db.Labors, "Id_labor", "Name", task.Labor);
            ViewBag.Lane = new SelectList(db.Lanes, "Id_lane", "Name", task.Lane);
            ViewBag.Location = new SelectList(db.Locations, "Id_location", "Name", task.Location);
            ViewBag.User = new SelectList(db.Users, "UserId", "Username", task.User);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Task_Id,Name,Date,Number_hours,Hour_type,User,Activity,Labor,Location")] Task task)
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
            ViewBag.Users = new SelectList(db.Users, "UserId", "Username", task.User);
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
