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
    public class LaborsController : Controller
    {
        private DB_Entities db = new DB_Entities();

        // GET: Labors
        public ActionResult Index(bool? deleted, bool? added, bool? updated)
        {
            var model = db.Labors.Include(u => u.ExtendedAttribute).ToList();


            if (deleted != null || added != null || updated != null)
            {
                if (deleted == true)
                {
                    ModelState.AddModelError("", "Labor eliminada satisfactoriamente");
                }
                else
                {
                    if (added == true)
                    {
                        ModelState.AddModelError("", "Labor agregada satisfactoriamente");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Labor editada satisfactoriamente");

                    }
                }
            }
            return View(model);
        }


        // GET: Labors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Labor labor = db.Labors.Find(id);
            if (labor == null)
            {
                return HttpNotFound();
            }
            return View(labor);
        }

        // GET: Labors/Create
        public ActionResult Create()
        {
            ViewBag.Id_ExtAttr = new SelectList(db.ExtendedAttributes, "Id_ExtAttr", "Name");
            
            return View();
        }

        // POST: Labors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Id_ExtAttr")] Labor labor)
        {
            ViewBag.Id_ExtAttr = new SelectList(db.ExtendedAttributes, "Id_ExtAttr", "Name", labor.Id_ExtAttr);


            if (labor.Name == null)
            {
                ModelState.AddModelError("", "Debe seleccionar un nombre para la labor ");
            }
            //if (labor.Id_ExtAttr == null)
            //{
            //    ModelState.AddModelError("", "Debe seleccionar un valor para el atributo extendido ");
            //}
            if (ModelState.IsValid)
            {
                try
                {
                    db.Labors.Add(labor);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { added = true });
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Algo salio mal, intente nuevamente");
                    return View(labor);
                }
            }



            

            return View(labor);
        }

        // GET: Labors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Labor labor = db.Labors.Find(id);
                if (labor == null)
                {
                    return HttpNotFound();
                }
                List<string> extAttr = new List<string>();

                if (labor.Id_ExtAttr == null)
                {
                    extAttr.Add("--");

                }
                else {
                    extAttr.Add(labor.ExtendedAttribute.Name);
                }
                ViewBag.Id_ExtAttr = new SelectList(extAttr);

                //ViewBag.Id_ExtAttr = new SelectList(db.ExtendedAttributes, "Id_ExtAttr", "Name", labor.Id_ExtAttr);



                return View(labor);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Algo salio mal, intente nuevamente");
                return RedirectToAction("Index", new { added = true });

            }
           
 
        }

        // POST: Labors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_labor,Name")] Labor labor)
        {
            ViewBag.Id_ExtAttr = new SelectList(db.ExtendedAttributes, "Id_ExtAttr", "Name", labor.Id_ExtAttr);

            if (labor.Name==null)
            {
                ModelState.AddModelError("", "Debe indicar un nombre para la labor");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var id_ExtAttr = db.Labors.Where(x => x.Id_labor == labor.Id_labor).Select(x => x.Id_ExtAttr).FirstOrDefault();
                    labor.Id_ExtAttr = id_ExtAttr;
                    db.Entry(labor).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { updated = true });
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Algo salio mal, intente nuevamente");

                    return View(labor);
                }
            }
            return View(labor);
        }

        // GET: Labors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Labor labor = db.Labors.Find(id);
            if (labor == null)
            {
                return HttpNotFound();
            }
            return View(labor);
        }

        // POST: Labors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Labor labor = db.Labors.Find(id);

            try
            {
                db.Labors.Remove(labor);
                db.SaveChanges();
                return RedirectToAction("Index", new { deleted = true });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "No es posible eliminar esta labor, compruebe que no este ligado a ninguna tarea registrada");

                return View(labor);
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
