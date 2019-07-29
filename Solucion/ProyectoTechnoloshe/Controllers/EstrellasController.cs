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
    public class EstrellasController : Controller
    {
        private TechnolosheProyectoEntities db = new TechnolosheProyectoEntities();

        // GET: Estrellas
        public ActionResult Index()
        {
            var estrellas = db.Estrellas.Include(e => e.Catalogo);
            return View(estrellas.ToList());
        }

        // GET: Estrellas/Create
        public ActionResult Create()
        {
            ViewBag.IDCatalogo = new SelectList(db.Catalogos, "ID", "Catalogo1");
            return View();


        }

        // POST: Estrellas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDCatalogo,Nombre,Tipo,DistanciaParsecs,MagnitudAbsoluta,VisibilidadHS")] Estrella estrella)
        {
            if (ModelState.IsValid)
            {
                db.Estrellas.Add(estrella);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDCatalogo = new SelectList(db.Catalogos, "ID", "Catalogo1", estrella.IDCatalogo);
            return View(estrella);
        }
        // GET: Estrellas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estrella estrella = db.Estrellas.Find(id);
            if (estrella == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCatalogo = new SelectList(db.Catalogos, "ID", "Catalogo1", estrella.IDCatalogo);
            return View(estrella);
        }

        // POST: Estrellas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDCatalogo,Nombre,Tipo,DistanciaParsecs,MagnitudAbsoluta,VisibilidadHS")] Estrella estrella)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estrella).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCatalogo = new SelectList(db.Catalogos, "ID", "Catalogo1", estrella.IDCatalogo);
            return View(estrella);
        }

        // GET: Estrellas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estrella estrella = db.Estrellas.Find(id);
            if (estrella == null)
            {
                return HttpNotFound();
            }
            return View(estrella);
        }

        // POST: Estrellas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estrella estrella = db.Estrellas.Find(id);
            db.Estrellas.Remove(estrella);
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
