using CopeyWinery.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CopeyWinery.Controllers
{
    public class StudentsController : Controller
    {
        private DB_Entities db = new DB_Entities();
        public ViewResult Index(string sortOrder, 
            string currentFilter, 
            string searchString,
            int? startYear,
            int? startMonth,
            int? page,   
            int? CurrentStartYearFilter,
            int? CurrentStartMonthFilter)
        {


            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (!String.IsNullOrEmpty(searchString))
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            if (startYear != null)
            {
                page = 1;
            }
            else
            {
                startYear = CurrentStartYearFilter;
            }

            if (startMonth != null)
            {
                page = 1;
            }
            else
            {
                startMonth = CurrentStartMonthFilter;
            }


            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentStartYearFilter = startYear;
            ViewBag.CurrentStartMonthFilter = startMonth;


            var tasks = from s in db.Tasks select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                tasks = tasks.Where(s => s.User.FirstName.Contains(searchString));
            }
            if (startYear != null)
            {
                tasks = tasks.Where(s => s.Date.Value.Year >= startYear);
            }
            if (startMonth != null)
            {
                tasks = tasks.Where(s => s.Date.Value.Month >= startMonth);
            }


            switch (sortOrder)
            {
                case "name_desc":
                    tasks = tasks.OrderByDescending(s => s.User.FirstName);
                    break;
                case "Date":
                    tasks = tasks.OrderBy(t => t.Date);
                    break;
                case "date_desc":
                    tasks = tasks.OrderByDescending(t => t.Date);
                    break;
                default:
                    tasks = tasks.OrderByDescending(t => t.Date);
                    break;
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(tasks.ToPagedList(pageNumber, pageSize));
        }
    }
}