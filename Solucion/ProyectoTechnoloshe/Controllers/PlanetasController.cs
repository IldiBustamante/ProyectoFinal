using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoTechnoloshe.Models;

namespace ProyectoTechnoloshe.Controllers
{
    public class PlanetasController : Controller
    {
        private TechnolosheProyectoEntities db = new TechnolosheProyectoEntities();

        // GET: Planetas
        public ActionResult Index()
        {
            var planetas = db.Planetas.Include(p => p.Catalogo);
            return View(planetas.ToList());
        }

        // GET: Planetas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planeta planeta = db.Planetas.Find(id);
            if (planeta == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCatalogo = new SelectList(db.Catalogos, "ID", "Catalogo1", planeta.IDCatalogo);
            return View(planeta);
        }

        // POST: Planetas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDCatalogo,Nombre,Tipo,DistanciaUA,MagnitudAbsoluta,VisibilidadHS")] Planeta planeta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planeta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCatalogo = new SelectList(db.Catalogos, "ID", "Catalogo1", planeta.IDCatalogo);
            return View(planeta);
        }

        // GET: Planetas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planeta planeta = db.Planetas.Find(id);
            if (planeta == null)
            {
                return HttpNotFound();
            }
            return View(planeta);
        }

        // POST: Planetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Planeta planeta = db.Planetas.Find(id);
            db.Planetas.Remove(planeta);
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
