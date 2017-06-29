using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppSASAcademico.Models;

namespace WebAppSASAcademico.Controllers
{
    public class TurmasController : Controller
    {
        private AcademicoContainer db = new AcademicoContainer();

        // GET: Turmas
        public ActionResult Index()
        {
            return View(db.Turma.ToList());
        }

        // GET: Turmas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turma turma = db.Turma.Find(id);
            if (turma == null)
            {
                return HttpNotFound();
            }
            return View(turma);
        }

        // GET: Turmas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Turmas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTurma,descricao,condicaoLimiteUm,condicaoLimiteDois")] Turma turma)
        {
            if (ModelState.IsValid)
            {
                db.Turma.Add(turma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(turma);
        }

        // GET: Turmas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turma turma = db.Turma.Find(id);
            if (turma == null)
            {
                return HttpNotFound();
            }
            return View(turma);
        }

        // POST: Turmas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTurma,descricao,condicaoLimiteUm,condicaoLimiteDois")] Turma turma)
        {
            if (ModelState.IsValid)
            {
                db.Entry(turma).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(turma);
        }

        // GET: Turmas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turma turma = db.Turma.Find(id);
            if (turma == null)
            {
                return HttpNotFound();
            }
            return View(turma);
        }

        // POST: Turmas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Turma turma = db.Turma.Find(id);
            db.Turma.Remove(turma);
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
