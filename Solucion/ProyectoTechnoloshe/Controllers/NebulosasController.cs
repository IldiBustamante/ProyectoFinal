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
    public class NebulosasController : Controller
    {
        private TechnolosheProyectoEntities db = new TechnolosheProyectoEntities();

        // GET: Nebulosas
        public ActionResult Index()
        {
            var nebulosas = db.Nebulosas.Include(n => n.Catalogo);
            return View(nebulosas.ToList());
        }
  
        // GET: Nebulosas/Create
        public ActionResult Create()
        {
            ViewBag.IDCatalogo = new SelectList(db.Catalogos, "ID", "Catalogo1");
            return View();
        }

        // POST: Nebulosas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDCatalogo,Nombre,Tipo,DistanciaParsecs,MagnitudAbsoluta,VisibilidadHS")] Nebulosa nebulosa)
        {
            if (ModelState.IsValid)
            {
                db.Nebulosas.Add(nebulosa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDCatalogo = new SelectList(db.Catalogos, "ID", "Catalogo1", nebulosa.IDCatalogo);
            return View(nebulosa);
        }

        // GET: Nebulosas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nebulosa nebulosa = db.Nebulosas.Find(id);
            if (nebulosa == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCatalogo = new SelectList(db.Catalogos, "ID", "Catalogo1", nebulosa.IDCatalogo);
            return View(nebulosa);
        }

        // POST: Nebulosas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDCatalogo,Nombre,Tipo,DistanciaParsecs,MagnitudAbsoluta,VisibilidadHS")] Nebulosa nebulosa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nebulosa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCatalogo = new SelectList(db.Catalogos, "ID", "Catalogo1", nebulosa.IDCatalogo);
            return View(nebulosa);
        }

        // GET: Nebulosas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nebulosa nebulosa = db.Nebulosas.Find(id);
            if (nebulosa == null)
            {
                return HttpNotFound();
            }
            return View(nebulosa);
        }

        // POST: Nebulosas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nebulosa nebulosa = db.Nebulosas.Find(id);
            db.Nebulosas.Remove(nebulosa);
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
