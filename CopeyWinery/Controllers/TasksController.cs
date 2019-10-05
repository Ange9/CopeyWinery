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
        public int Activity { get; set; }
        public int Labor { get; set; }
        public int? Ext_Attr { get; set; }
        public int Location { get; set; }
        public int User { get; set; }


    }
    public class TasksController : Controller
    {
        private DB_Entities db = new DB_Entities();

        //public ViewResult Index( string searchString)
        //{

        //    var tasks = from s in db.Tasks
        //                   select s;
        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        tasks = tasks.Where(s => s.User.Username.Contains(searchString));
        //    }


        //    return View(tasks.ToList());
        //}

        //GET: Tasks
       [Authorize]
        public ActionResult Index(bool? deleted, bool? added, bool? updated, bool? addFailed, string searchString, DateTime? startDate, DateTime? endDate)
        {
            ViewBag.start = startDate;
            ViewBag.end = endDate;

            if (deleted != null || added != null || updated != null)
            {
                if (deleted == true)
                {
                    ModelState.AddModelError("", "Tarea eliminada satisfactoriamente");
                }
                else
                {
                    if (updated == true)
                    {
                        ModelState.AddModelError("", "Tarea editada satisfactoriamente");
                    }
                    else
                    {
                        if (added != null)
                        {
                            ModelState.AddModelError("", "Tarea agregada satisfactoriamente");

                        }
                        else
                        {
                            ModelState.AddModelError("", "No se ha podido agregar la tarea");

                        }

                    }
                }
            }
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");

            }
            var tasks = from s in db.Tasks
                        select s;

            if (!User.IsInRole("Administrator"))
            {
                tasks = tasks.Where(s => s.User.FirstName.Contains(searchString))
                    .Where(x => x.User.Username == User.Identity.Name);
                foreach (Task task in tasks) {
                    task.Date.Value.ToString("dd/MM/yyyy");
                }
                return View(tasks.ToList());

            }
            else
            {
                if (!String.IsNullOrEmpty(searchString))
                {                
                    tasks = tasks.Where(s => s.User.FirstName.Contains(searchString));
                }
                if (startDate != null) {
                    tasks = tasks.Where(s => s.Date >= (startDate));
                }
                if (endDate != null)
                {
                    tasks = tasks.Where(s => s.Date <= (endDate));
                }
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
            ViewBag.Activity_Id = new SelectList(db.Activities, "Activity_Id", "Activity_name");
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Activity(Task model)
        {

            if (model.Activity_Id == 0)
            {
                ModelState.AddModelError("", "Debe seleccionar una actividad!!!");
            }
            if (ModelState.IsValid)
            {
              
                TaskObject taskObject = new TaskObject();
                taskObject.Date = model.Date;
                taskObject.Number_hours = model.Number_hours;
                taskObject.Hour_type = model.Hour_type;
                taskObject.Activity = model.Activity_Id;
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
            task.Activity_Id = taskObj.Activity;
            ViewBag.Id_labor = new SelectList(db.Labors, "Id_labor", "Name");
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Labor(Task model)
        {

            if (model.Id_labor == 0 )
            {
                ModelState.AddModelError("", "Debe seleccionar una labor!!!");
            }
            if (ModelState.IsValid)
            {

                TaskObject taskObject = new TaskObject();
                taskObject.Date = model.Date;
                taskObject.Number_hours = model.Number_hours;
                taskObject.Hour_type = model.Hour_type;
                taskObject.Activity = model.Activity_Id;
                taskObject.Labor = model.Id_labor;
                return RedirectToAction("Add_Ext_Attr", taskObject);
            }
            return View(model);
        }
        public ActionResult Add_Ext_Attr(TaskObject taskObj)
        {
            Task task = new Task();
            task.Date = taskObj.Date;
            task.Number_hours = taskObj.Number_hours;
            task.Hour_type = taskObj.Hour_type;
            task.Activity_Id = taskObj.Activity;
            task.Id_labor = taskObj.Labor;
            var ex = db.Labors.Include(e => e.ExtendedAttribute)
                            .Where(x => x.Id_labor==task.Id_labor)
                            .Select(x=> x.ExtendedAttribute.Name)                          
                            .FirstOrDefault();
            ViewBag.Ext_Attr = ex;
            if (ex==null)
            {
                return RedirectToAction("Add_Location", taskObj);
            }
            return View(task);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Ext_Attr(Task model)
        {

            if (model.Ext_Attr_Labor_Value == null)
            {
                ModelState.AddModelError("", "Debe seleccionar un valor");
            }


            if (ModelState.IsValid)
            {
                TaskObject taskObject = new TaskObject();
                taskObject.Date = model.Date;
                taskObject.Number_hours = model.Number_hours;
                taskObject.Hour_type = model.Hour_type;
                taskObject.Activity = model.Activity_Id;
                taskObject.Labor = model.Id_labor;
                taskObject.Ext_Attr = model.Ext_Attr_Labor_Value;
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
            task.Activity_Id = taskObj.Activity;
            task.Id_labor = taskObj.Labor;
            task.Ext_Attr_Labor_Value = taskObj.Ext_Attr;
            ViewBag.Id_location = new SelectList(db.Locations, "Id_location", "Name");
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Location(Task model)
        {

            if (model.Id_location == 0)
            {
                ModelState.AddModelError("", "Debe seleccionar una ubicacion");
            }
            if (ModelState.IsValid)
            {

                TaskObject taskObject = new TaskObject();
                taskObject.Date = model.Date;
                taskObject.Number_hours = model.Number_hours;
                taskObject.Hour_type = model.Hour_type;
                taskObject.Activity = model.Activity_Id;
                taskObject.Labor = model.Id_labor;
                taskObject.Ext_Attr = model.Ext_Attr_Labor_Value;
                taskObject.Location = model.Id_location;
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
            task.Activity_Id = taskObj.Activity;
            task.Id_labor = taskObj.Labor;
            task.Ext_Attr_Labor_Value = taskObj.Ext_Attr;
            task.Id_location = taskObj.Location;
            task.Name = task.Task_Id.ToString();
            try
            {
                task.UserId = db.User.Where(x => x.Username == User.Identity.Name).Select(x => x.UserId).FirstOrDefault();
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index", new { added = true });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Algo salio mal, intente nuevamente");
                return RedirectToAction("Index", new { addFailed = true });
            }
        }

        public ActionResult EditTest(int? id)
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
            else
            {
                hourTypeOption.Add("Extraordinaria");
                hourTypeOption.Add("Ordinaria");
            }



            ViewBag.Hour_type = new SelectList(hourTypeOption);
            ViewBag.Activity_Id = new SelectList(db.Activities, "Activity_Id", "Activity_name", task.Activity_Id);
            ViewBag.Id_labor = new SelectList(db.Labors, "Id_labor", "Name", task.Id_labor);
            ViewBag.Id_location = new SelectList(db.Locations, "Id_location", "Name", task.Id_location);
            ViewBag.UserId = new SelectList(db.User, "UserId", "FirstName", task.UserId);
            var ex = db.Labors.Include(e => e.ExtendedAttribute)
                .Where(x => x.Id_labor == task.Id_labor)
                .Select(x => x.ExtendedAttribute.Name)
                .FirstOrDefault();
            ViewBag.Ext_Attr = ex;
            return View(task);
        }

        public ActionResult ExportView()
        {
            Response.AddHeader("content-disposition", "attachment;filename=Report1.xls");
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            return View(db.Tasks.AsEnumerable());
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
            List<string> hourTypeOption = new List<string>();
            if (task.Hour_type == "Ordinaria")
            {
                hourTypeOption.Add("Ordinaria");
                hourTypeOption.Add("Extraordinaria");
            }
            else
            {
                hourTypeOption.Add("Extraordinaria");
                hourTypeOption.Add("Ordinaria");
            }


            ViewBag.Hour_type = new SelectList(hourTypeOption);
            ViewBag.Activity_Id = new SelectList(db.Activities, "Activity_Id", "Activity_name", task.Activity_Id);
            ViewBag.Id_labor = new SelectList(db.Labors, "Id_labor", "Name", task.Id_labor);
            ViewBag.Id_location = new SelectList(db.Locations, "Id_location", "Name", task.Id_location);
            ViewBag.UserId = new SelectList(db.User, "UserId", "FirstName", task.UserId);
            var ex = db.Labors.Include(e => e.ExtendedAttribute)
                .Where(x => x.Id_labor == task.Id_labor)
                .Select(x => x.ExtendedAttribute.Name)
                .FirstOrDefault();
            ViewBag.Ext_Attr = ex;
            return View(task);
        }

        // POST: Tasks1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Task_Id,Name,Date,Number_hours,Hour_type,UserId,Activity_Id,Id_labor,Id_location,Ext_Attr_Labor_Value")] Task task)
        {

            if (task.Date== null || task.Number_hours==null || task.Hour_type==null )
            {
                ModelState.AddModelError("", "Todos los campos son requeridos");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(task).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { updated = true });
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Algo salio mal, intente nuevamente");

                    return View(task);
                }
                
            }
            ViewBag.Activity_Id = new SelectList(db.Activities, "Activity_Id", "Activity_name", task.Activity_Id);
            ViewBag.Id_labor = new SelectList(db.Labors, "Id_labor", "Name", task.Id_labor);
            ViewBag.Id_location = new SelectList(db.Locations, "Id_location", "Name", task.Id_location);
            ViewBag.UserId = new SelectList(db.User, "UserId", "Username", task.UserId);
            return View(task);
        }

        //// GET: Tasks/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Task task = db.Tasks.Find(id);
        //    if (task == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    List<string> hourTypeOption = new List<string>();
        //    if (task.Hour_type == "Ordinaria")
        //    {
        //        hourTypeOption.Add("Ordinaria");
        //        hourTypeOption.Add("Extraordinaria");
        //    }
        //    else {
        //        hourTypeOption.Add("Extraordinaria");
        //        hourTypeOption.Add("Ordinaria");
        //    }


        //    ViewBag.Hour_type = new SelectList(hourTypeOption);
        //    //ViewBag.Activity = new SelectList(db.Activities, "Activity_Id", "Activity_name", task.Activity);
        //    //ViewBag.Labor = new SelectList(db.Labors, "Id_labor", "Name", task.Labor);
        //    //ViewBag.Location = new SelectList(db.Locations, "Id_location", "Name", task.Location);
        //    //ViewBag.Users = new SelectList(db.User, "UserId", "FirstName", task.User);

        //    ViewBag.Activity = new SelectList(db.Activities, "Activity_Id", "Activity_name", task.Activity_Id);
        //    ViewBag.Labor = new SelectList(db.Labors, "Id_labor", "Name", task.Id_labor);
        //    ViewBag.Location = new SelectList(db.Locations, "Id_location", "Name", task.Id_location);
        //    ViewBag.UserId = new SelectList(db.User, "UserId", "Username", task.UserId);

        //    return View(task);
        //}

        //// POST: Tasks/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Task_Id,Name,Date,Number_hours,Hour_type,User_Id,Activity_Id,Id_labor,Id_location")] Task task)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(task).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.Hour_type = new SelectList(task.Hour_type);
        //    ViewBag.Activity = new SelectList(db.Activities, "Activity_Id", "Activity_name", task.Activity_Id);
        //    ViewBag.Labor = new SelectList(db.Labors, "Id_labor", "Name", task.Id_labor);
        //    ViewBag.Location = new SelectList(db.Locations, "Id_location", "Name", task.Id_location);
        //    ViewBag.UserId = new SelectList(db.User, "UserId", "Username", task.UserId);
        //    return View(task);
        //}

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
            try
            {
                db.Tasks.Remove(task);
                db.SaveChanges();
                return RedirectToAction("Index", new { deleted = true });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "EN este momento no es posible eliminar esta tarea");

                return View(task);
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
