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
    public class ExtendedAttributesController : Controller
    {
        private DB_Entities db = new DB_Entities();

        // GET: ExtendedAttributes
        public ActionResult Index(bool? deleted, bool? added, bool? updated)
        {
            var model = db.ExtendedAttributes.ToList();
            if (deleted != null || added != null || updated != null)
            {
                if (deleted == true)
                {
                    ModelState.AddModelError("", "Atributo eliminado satisfactoriamente");
                }
                else
                {
                    if (added == true)
                    {
                        ModelState.AddModelError("", "Atributo agregado satisfactoriamente");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Atributo editado satisfactoriamente");

                    }
                }
            }
            return View(model);
        }

        // GET: ExtendedAttributes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtendedAttribute extendedAttribute = db.ExtendedAttributes.Find(id);
            if (extendedAttribute == null)
            {
                return HttpNotFound();
            }
            return View(extendedAttribute);
        }

        // GET: ExtendedAttributes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExtendedAttributes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_ExtAttr,Name")] ExtendedAttribute extendedAttribute)
        {
            if (extendedAttribute.Name == null)
            {
                ModelState.AddModelError("", "Debe indicar un nombre para la atributo");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.ExtendedAttributes.Add(extendedAttribute);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { added = true });
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Algo salio mal, intente nuevamente");

                    return View(extendedAttribute);
                }
            }
            return View(extendedAttribute);
        }

        // GET: ExtendedAttributes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtendedAttribute extendedAttribute = db.ExtendedAttributes.Find(id);
            if (extendedAttribute == null)
            {
                return HttpNotFound();
            }
            return View(extendedAttribute);
        }

        // POST: ExtendedAttributes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_ExtAttr,Name")] ExtendedAttribute extendedAttribute)
        {

            if (extendedAttribute.Name == null)
            {
                ModelState.AddModelError("", "Debe indicar un nombre para el atributo");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(extendedAttribute).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { updated = true });
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Algo salio mal, intente nuevamente");

                    return View(extendedAttribute);
                }
            }
            return View(extendedAttribute);
        }

        // GET: ExtendedAttributes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtendedAttribute extendedAttribute = db.ExtendedAttributes.Find(id);
            if (extendedAttribute == null)
            {
                return HttpNotFound();
            }
            return View(extendedAttribute);
        }

        // POST: ExtendedAttributes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExtendedAttribute extendedAttribute = db.ExtendedAttributes.Find(id);
            try
            {
                db.ExtendedAttributes.Remove(extendedAttribute);
                db.SaveChanges();
                return RedirectToAction("Index", new { deleted = true });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "No es posible eliminar este atributo, compruebe que no este ligado a ninguna labor");

                return View(extendedAttribute);
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
