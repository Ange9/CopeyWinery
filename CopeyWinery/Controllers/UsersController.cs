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
    public class UsersController : Controller
    {
        private DB_Entities db = new DB_Entities();

        // GET: Users
        public ActionResult Index(bool? deleted, bool? added, bool? updated)
        {
            var model = db.User.Include(u => u.Role).ToList();


            if (deleted != null || added != null || updated != null)
            {
                if (deleted == true)
                {
                    ModelState.AddModelError("", "Usuario eliminado satisfactoriamente");
                }
                else
                {
                    if (added == true)
                    {
                        ModelState.AddModelError("", "Usuario agregado satisfactoriamente");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Usuario editado satisfactoriamente");

                    }
                }
            }
            return View(model);
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName, Username,Password,RoleId")] User user)
        {

            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", user.RoleId);

            if (user.FirstName == null || user.Username== null || user.Password== null)
            {
                ModelState.AddModelError("", "Todos los campos son obligatorios ");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    user.CreatedDate = DateTime.UtcNow;
                    db.User.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { added = true });
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Algo salio mal, intente nuevamente");
                    return View(user);
                }
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,FirstName,Username,Password,RoleId,CreatedDate")] User user)
        {
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", user.RoleId);
            if (user.FirstName == null || user.Username == null || user.Password == null)
            {
                ModelState.AddModelError("", "Debe indicar un nombre para la actividad");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { updated = true });
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Algo salio mal, intente nuevamente");

                    return View(user);
                }
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.User.Find(id);

            try
            {
                db.User.Remove(user);
                db.SaveChanges();
                return RedirectToAction("Index", new { deleted = true });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "No es posible eliminar esta usuario, compruebe que no este ligado a ninguna tarea registrada");

                return View(user);
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
