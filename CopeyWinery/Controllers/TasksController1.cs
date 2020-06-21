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
using PagedList;


namespace CopeyWinery.Controllers
{
    public class TasksController1 : Controller
    {
        private DB_Entities db = new DB_Entities();

        public ActionResult Index(DateTime? start, DateTime? end, int? page)
        {
            ViewBag.start = start;
            ViewBag.end = end;
            var tasks = db.Tasks
                .Where(x => x.Date > start
                && x.Date < end)
                .OrderByDescending(x => x.Date)
                .ToList();
            page = 1;

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(tasks.ToPagedList(pageNumber, pageSize));
        }



    }
}
