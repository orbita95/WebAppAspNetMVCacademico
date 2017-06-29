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
    public class ExerciciosController : Controller
    {
        private AcademicoContainer db = new AcademicoContainer();

        // GET: Exercicios
        public ActionResult Index()
        {
            var exercicio = db.Exercicio.Include(e => e.ListaExercicios);
            return View(exercicio.ToList());
        }

        // GET: Exercicios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercicio exercicio = db.Exercicio.Find(id);
            if (exercicio == null)
            {
                return HttpNotFound();
            }
            return View(exercicio);
        }

        // GET: Exercicios/Create
        public ActionResult Create()
        {
            ViewBag.IdListaExercicios = new SelectList(db.Atividade, "IdListaExercicio", "descricao");
            return View();
        }

        // POST: Exercicios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdExercicio,enunciado,respostaAluno,respostaProfessor,comentario,IdListaExercicios")] Exercicio exercicio)
        {
            if (ModelState.IsValid)
            {
                db.Exercicio.Add(exercicio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdListaExercicios = new SelectList(db.Atividade, "IdListaExercicio", "descricao", exercicio.IdListaExercicios);
            return View(exercicio);
        }

        // GET: Exercicios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercicio exercicio = db.Exercicio.Find(id);
            if (exercicio == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdListaExercicios = new SelectList(db.Atividade, "IdListaExercicio", "descricao", exercicio.IdListaExercicios);
            return View(exercicio);
        }

        // POST: Exercicios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdExercicio,enunciado,respostaAluno,respostaProfessor,comentario,IdListaExercicios,identificador")] Exercicio exercicio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exercicio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdListaExercicios = new SelectList(db.Atividade, "IdListaExercicio", "descricao", exercicio.IdListaExercicios);
            return View(exercicio);
        }

        // GET: Exercicios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercicio exercicio = db.Exercicio.Find(id);
            if (exercicio == null)
            {
                return HttpNotFound();
            }
            return View(exercicio);
        }

        // POST: Exercicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exercicio exercicio = db.Exercicio.Find(id);
            db.Exercicio.Remove(exercicio);
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
