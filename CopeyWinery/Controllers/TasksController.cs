using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.SqlServer;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using CopeyWinery.Models;
using PagedList;


namespace CopeyWinery.Controllers
{
    public class TaskObject
    {
        public DateTime? Date { get; set; }
        public String Number_hours { get; set; }
        public string Hour_type { get; set; }
        public int Activity { get; set; }
        public int Labor { get; set; }
        public String Ext_Attr { get; set; }
        public int Location { get; set; }
        public int User { get; set; }


    }
    public class TasksController : Controller
    {
        private DB_Entities db = new DB_Entities();



        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page,
         bool? deleted, bool? added, bool? updated, bool? addFailed, int startMonth = 0, int startYear = 0)
        {
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

            ViewBag.StartMonth = startMonth;
            ViewBag.StartYear = startYear;

            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");

            }

            ViewBag.CurrentFilter = searchString;
            if (currentFilter != startMonth.ToString()) {
                ViewBag.CurrentFilter = startMonth.ToString();
                page = 1;
            }
            if (!User.IsInRole("Administrator"))
            {
                var tasks = db.Tasks.Where(x => x.User.Username == User.Identity.Name).AsEnumerable().OrderBy(t => t.Date);

                int pageSize = 2;
                int pageNumber = (page ?? 1);
                return View(tasks.ToPagedList(pageNumber, pageSize));
               
            }
            if (startMonth != 0 && startYear != 0)
            {

                var tasks = db.Tasks
                    .Where(x => x.Date.Value.Month == startMonth && x.Date.Value.Year == startYear)
                    .OrderBy(x=> x.Date);

                Session["tasks"] = tasks.ToList();
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(tasks.ToPagedList(pageNumber, pageSize));
            }
            else {
                var tasks = db.Tasks
                   .Where(x => x.Date.Value.Month == DateTime.Now.Month && x.Date.Value.Year == DateTime.Now.Year)
                   .OrderBy(x => x.Date); ;
                Session["tasks"] = tasks.ToList();

                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(tasks.ToPagedList(pageNumber, pageSize));
            }

        }


        //// public ActionResult Index(string sortOrder, string currentFilter, string searchString, DateTime? startDate, DateTime? endDate, int? page)
        //public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, DateTime? startDate, DateTime? endDate, bool? deleted, bool? added, bool? updated, bool? addFailed)

        //{

        //    ViewBag.start = startDate;
        //    ViewBag.end = endDate;

        //    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "name_asc";
        //    ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

        //    ViewBag.CurrentSort = sortOrder;


        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Index", "Home");

        //    }

        //    if (searchString != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        searchString = currentFilter;
        //    }

        //    ViewBag.CurrentFilter = searchString;

        //    var tasks = from s in db.Tasks select s ;

        //    if (!User.IsInRole("Administrator"))
        //    {
        //        tasks = tasks.Where(x => x.User.Username == User.Identity.Name);
        //    }
        //    else
        //    {
        //        if (!String.IsNullOrEmpty(searchString))
        //        {
        //            tasks = tasks.Where(s => s.User.FirstName.Contains(searchString));
        //        }
        //        if (startDate != null)
        //        {
        //            tasks = tasks.Where(s => s.Date >= (startDate));
        //        }
        //        if (endDate != null)
        //        {
        //            tasks = tasks.Where(s => s.Date <= (endDate));
        //        }
        //    }


        //    switch (sortOrder)
        //    {
        //        case "name_desc":
        //            tasks = tasks.OrderByDescending(t => t.User.FirstName);
        //            break;
        //        case "name_asc":
        //            tasks = tasks.OrderBy(t => t.User.FirstName);       
        //            break;
        //        case "Date":
        //            tasks = tasks.OrderBy(t => t.Date);
        //            break;
        //        case "date_desc":
        //            tasks = tasks.OrderByDescending(t => t.Date);
        //            break;
        //        default:
        //            tasks = tasks.OrderByDescending(t => t.Date);
        //            break;
        //    }

        //    int pageSize = 10;
        //    int pageNumber = (page ?? 1);
        //    return View(tasks.ToPagedList(pageNumber, pageSize));
        //}


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
        //[Authorize]
        // public ActionResult Index(bool? deleted, bool? added, bool? updated, bool? addFailed, string searchString, DateTime? startDate, DateTime? endDate)
        // {
        //     try
        //     {
        //         ViewBag.start = startDate;
        //         ViewBag.end = endDate;

        //         if (deleted != null || added != null || updated != null)
        //         {
        //             if (deleted == true)
        //             {
        //                 ModelState.AddModelError("", "Tarea eliminada satisfactoriamente");
        //             }
        //             else
        //             {
        //                 if (updated == true)
        //                 {
        //                     ModelState.AddModelError("", "Tarea editada satisfactoriamente");
        //                 }
        //                 else
        //                 {
        //                     if (added != null)
        //                     {
        //                         ModelState.AddModelError("", "Tarea agregada satisfactoriamente");

        //                     }
        //                     else
        //                     {
        //                         ModelState.AddModelError("", "No se ha podido agregar la tarea");

        //                     }

        //                 }
        //             }
        //         }
        //         if (!User.Identity.IsAuthenticated)
        //         {
        //             return RedirectToAction("Index", "Home");

        //         }
        //         var tasks = from s in db.Tasks
        //                     select s;

        //         if (!User.IsInRole("Administrator"))
        //         {
        //             tasks = tasks.Where(x => x.User.Username == User.Identity.Name);
        //             foreach (Task task in tasks)
        //             {
        //                 task.Date.Value.ToString("dd/MM/yyyy");
        //             }
        //             return View(tasks.ToList());

        //         }
        //         else
        //         {
        //             if (!String.IsNullOrEmpty(searchString))
        //             {
        //                 tasks = tasks.Where(s => s.User.FirstName.Contains(searchString));
        //             }
        //             if (startDate != null)
        //             {
        //                 tasks = tasks.Where(s => s.Date >= (startDate));
        //             }
        //             if (endDate != null)
        //             {
        //                 tasks = tasks.Where(s => s.Date <= (endDate));
        //             }
        //             return View(tasks.ToList());
        //         }
        //     }
        //     catch (Exception)
        //     {
        //         ModelState.AddModelError("", "Algo salio mal, intente nuevamente");
        //         return RedirectToAction("Index", "Home");
        //     }


        // }

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

        public ActionResult Create(TaskObject taskObj)
        {
            Task task = new Task();
            task.Number_hours = taskObj.Number_hours;
            task.Hour_type = taskObj.Hour_type;
            task.Activity_Id = taskObj.Activity;
            task.Id_labor = taskObj.Labor;
            task.Ext_Attr_Labor_Value = taskObj.Ext_Attr;
            task.Id_location = taskObj.Location;
            return View(task);
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

        public ActionResult Add_Num_Hours(TaskObject taskObj)
        {
            Task task = new Task();
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
                taskObject.Number_hours = model.Number_hours;
                return RedirectToAction("Add_Hour_type", taskObject);
            }
            return View(model);
        }

        public ActionResult Add_Hour_type(TaskObject taskObj)
        {
            Task task = new Task();
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
                taskObject.Number_hours = model.Number_hours;
                taskObject.Hour_type= model.Hour_type;
                return RedirectToAction("Add_Activity", taskObject);
            }
            return View(model);
        }


        public ActionResult Add_Activity(TaskObject taskObj)
        {
            Task task = new Task();
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
                taskObject.Number_hours = model.Number_hours;
                taskObject.Hour_type = model.Hour_type;
                taskObject.Activity = model.Activity_Id;
                taskObject.Labor = model.Id_labor;
                taskObject.Ext_Attr = model.Ext_Attr_Labor_Value;
                taskObject.Location = model.Id_location;
                return RedirectToAction("Create", taskObject);
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
            task.Ext_Attr_Labor_Value =taskObj.Ext_Attr;
            task.Id_location = taskObj.Location;
            task.Name = task.Task_Id.ToString();
            try
            {
                task.UserId = db.User.Where(x => x.Username == User.Identity.Name).Select(x => x.UserId).FirstOrDefault();
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index", new { added = true });
            }
            catch (Exception ex)
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
            var taskList = (List<Task>)Session["tasks"];



                 var query = from s in taskList
                             join act in db.Activities on s.Activity_Id equals act.Activity_Id
                             join lab in db.Labors on s.Id_labor equals lab.Id_labor
                             join ext in db.ExtendedAttributes on lab.Id_ExtAttr equals ext.Id_ExtAttr
                             join usr in db.User on s.UserId equals usr.UserId
                             join loc in db.Locations on s.Id_location equals loc.Id_location
                             select new {Fecha = s.Date.Value.ToString("dd/MM/yyyy"), Actividad = act.Activity_name, Labor= lab.Name,
                                 Atributo_Extendido = ext.Name,Valor_Atributo_Extendido = s.Ext_Attr_Labor_Value, Ubicacion = loc.Name,
                                 Cant_Horas = s.Number_hours, Tipo = s.Hour_type,  Colaborador = usr.FirstName};

            Dictionary<int, string> months = new Dictionary<int, string>();
            months.Add(1, "Enero");
            months.Add(2, "Febrero");
            months.Add(3, "Marzo");
            months.Add(4, "Abril");
            months.Add(5, "Mayo");
            months.Add(6, "Junio");
            months.Add(7, "Julio");
            months.Add(8, "Agosto");
            months.Add(9, "Setiembre");
            months.Add(10, "Octubre");
            months.Add(11, "Noviembre");
            months.Add(12, "Diciembre");

            int month = taskList.Select(x => x.Date.Value.Month).FirstOrDefault();
            int year = taskList.Select(x => x.Date.Value.Year).FirstOrDefault();

            GridView grid = new GridView();
            grid.DataSource = query;

            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment; filename=Employees.xls");
            //Response.ContentType = "application/ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename="+ months[month]+ "-"+year+ ".xls");
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("Index");
        
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
        public ActionResult Edit([Bind(Include = "Task_Id,Name,Date,Number_hours,Hour_type,UserId,Activity_Id,Id_location")] Task task)
        {

            if (task.Date== null || task.Number_hours==null || task.Hour_type==null )
            {
                ModelState.AddModelError("", "Todos los campos son requeridos");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var id_labor = db.Tasks.Where(x => x.Task_Id == task.Task_Id).Select(x => x.Id_labor).FirstOrDefault();
                    var ext_attr = db.Tasks.Where(x => x.Task_Id == task.Task_Id).Select(x => x.Labor.Id_ExtAttr).FirstOrDefault();
                    var ext_Attr_Labor_Value = db.Tasks.Where(x => x.Task_Id == task.Task_Id).Select(x => x.Ext_Attr_Labor_Value).FirstOrDefault();

                    task.Id_labor = id_labor;
                    task.Ext_Attr_Labor_Value = ext_Attr_Labor_Value;

                    db.Entry(task).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { updated = true });
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "Algo salio mal, intente nuevamente");

                    return RedirectToAction("Index", new { updated = false });
                }

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
            ViewBag.UserId = new SelectList(db.User, "UserId", "Username", task.UserId);
           

            try
            {
                var id_labor = db.Tasks.Where(x => x.Task_Id == task.Task_Id).Select(x => x.Id_labor).FirstOrDefault();
                task.Id_labor = id_labor;

                task.Ext_Attr_Labor_Value = db.Tasks.Where(x => x.Task_Id == task.Task_Id).Select(x => x.Ext_Attr_Labor_Value).FirstOrDefault();

                var ex = db.Labors.Include(e => e.ExtendedAttribute)
                .Where(x => x.Id_labor == task.Id_labor)
                .Select(x => x.ExtendedAttribute.Name)
                .FirstOrDefault();
                ViewBag.Ext_Attr = ex;
                
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Algo salio mal, intente nuevamente");

                return RedirectToAction("Index", new { updated = false });
            }


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
