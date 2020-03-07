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
            string clearSearch,
            string startYear,
            string startMonth,
            string endYear,
            string endMonth,
            int? page,   
            string currentStartYearFilter,
            string currentStartMonthFilter,
            string currentEndYearFilter,
            string currentEndMonthFilter
            )
        {


         

            List<SelectListItem> months = new List<SelectListItem>();

            months.Add(new SelectListItem { Value = "0", Text = "Mes" });
            months.Add(new SelectListItem { Value = "1", Text = "Enero" });
            months.Add(new SelectListItem { Value = "2", Text = "Febrero" });
            months.Add(new SelectListItem { Value = "3", Text = "Marzo" });
            months.Add(new SelectListItem { Value = "4", Text = "Abril" });
            months.Add(new SelectListItem { Value = "5", Text = "Mayo" });
            months.Add(new SelectListItem { Value = "6", Text = "Junio" });
            months.Add(new SelectListItem { Value = "7", Text = "Julio" });
            months.Add(new SelectListItem { Value = "8", Text = "Agosto" });

            ViewBag.Months = months;

            List<SelectListItem> years = new List<SelectListItem>();
            years.Add(new SelectListItem { Value = "0", Text = "Año" });
            int year = DateTime.Now.Year - 5;
            for (int i=1;  i <= 5; i++) {
                years.Add(new SelectListItem { Value = i.ToString(), Text = (year + i).ToString() });
            }

            ViewBag.Years = years;



            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";



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
                startYear = currentStartYearFilter;
  
            }

            if (startMonth != null)
            {
                page = 1;
            }
            else
            {
                startMonth = currentStartMonthFilter;
            }


            //if (endYear != null)
            //{
            //    page = 1;
            //}
            //else
            //{
            //    endYear = currentEndYearFilter;
            //}

            //if (endMonth != null)
            //{
            //    page = 1;
            //}
            //else
            //{
            //    endMonth = currentEndMonthFilter;
            //}


            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentStartYearFilter = startYear;
            ViewBag.CurrentStartMonthFilter = startMonth;
            ViewBag.CurrentEndYearFilter = endYear;
            ViewBag.CurrentEndMonthFilter = endMonth;


            var tasks = from s in db.Tasks select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                tasks = tasks.Where(s => s.User.FirstName.Contains(searchString));
            }
            if (startYear != null)
            {
                int startYearInt= Convert.ToInt32(startYear);
                tasks = tasks
                    .Where(s => s.Date.Value.Year >= startYearInt).ToList();


                tasks = (from x in tasks
                         .Where(x.)

            }
            //if (startMonth != null)
            //{
            //    tasks = tasks.Where(s => s.Date.Value.Month >= Int32.Parse(startMonth));
            //}

            //if (endYear != null)
            //{
            //    tasks = tasks.Where(s => s.Date.Value.Year <= endYear);
            //}
            //if (endMonth != null)
            //{
            //    tasks = tasks.Where(s => s.Date.Value.Month <= endMonth);
            //}


            //switch (sortOrder)
            //{
            //    case "name_desc":
            //        tasks = tasks.OrderByDescending(s => s.User.FirstName);
            //        break;
            //    case "Date":
            //        tasks = tasks.OrderBy(t => t.Date);
            //        break;
            //    case "date_desc":
            //        tasks = tasks.OrderByDescending(t => t.Date);
            //        break;
            //    default:
            //        tasks = tasks.OrderByDescending(t => t.Date);
            //        break;
            //}

            tasks = tasks.OrderByDescending(t => t.Date);
            if ((startMonth != null && startYear !=null) || (endYear != null && endMonth!=null )){
                tasks = tasks.OrderBy(t => t.Date);
            }


            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(tasks.ToPagedList(pageNumber, pageSize));
        }
    }
}