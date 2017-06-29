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
    public class ListaExerciciosController : Controller
    {
        private AcademicoContainer db = new AcademicoContainer();
        private GerenciaConsultasExercicios gce = new GerenciaConsultasExercicios(); 

        //retorna as listas enviadas para correção
        public ActionResult ListasACorrigir()
        {
            int estado = 1; //listar para correção
            var listas = db.Atividade.Where(l => l.estado == estado).ToList();

            return View(listas);
        }

        public ActionResult ExerciciosListaParaCorrecao(int id)
        {
            var exercicios = gce.getExerciciosListaParaCorrecao(id);//exercicios de uma lista 
            
            //TempData["ListaExercicioAtual"] = lista;
            
            return View(exercicios);
        }

        // GET: ListaExercicios
        public ActionResult Index()
        {
            var atividade = db.Atividade.Include(l => l.Turma);
            return View(atividade.ToList());
        }

        // GET: ListaExercicios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaExercicios listaExercicios = db.Atividade.Find(id);
            if (listaExercicios == null)
            {
                return HttpNotFound();
            }
            return View(listaExercicios);
        }

        // GET: ListaExercicios/Create
        public ActionResult Create()
        {
            ViewBag.IdTurma = new SelectList(db.Atividade.Where(l => l.estado == null), "IdTurma", "descricao");
            return View();
        }

        // POST: ListaExercicios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdListaExercicio,nota,descricao,nomeAluno,idAluno,IdTurma")] ListaExercicios listaExercicios)
        {
            if (ModelState.IsValid)
            {
                listaExercicios.identificadorListaOriginal = listaExercicios.IdListaExercicio;

                //retorna o numero da ultima lista criada pelo professor
                var listas = gce.getListasOriginaisByTurma(listaExercicios.IdTurma);//.Last().numeroListaTurma;
                var numero = listas.Last().numeroListaTurma;
                                
                listaExercicios.numeroListaTurma = numero + 1;//identifica a lista
                db.Atividade.Add(listaExercicios);
                db.SaveChanges();

                //identifica o campo identificadorListaOriginal com o id da lista criada
                var ultimaLista = gce.getListasOriginaisByTurma(listaExercicios.IdTurma).Last();
                listaExercicios.identificadorListaOriginal = ultimaLista.IdListaExercicio;
                db.Entry(listaExercicios).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.IdTurma = new SelectList(db.Turma, "IdTurma", "descricao", listaExercicios.IdTurma);
            return View(listaExercicios);
        }

        // GET: ListaExercicios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaExercicios listaExercicios = db.Atividade.Find(id);
            if (listaExercicios == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdTurma = new SelectList(db.Turma, "IdTurma", "descricao", listaExercicios.IdTurma);
            return View(listaExercicios);
        }

        // POST: ListaExercicios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdListaExercicio,nota,descricao,nomeAluno,idAluno,IdTurma,identificadorListaOriginal,estado,numeroListaTurma")] ListaExercicios listaExercicios)
        {
            if (ModelState.IsValid)
            {
                //var lista = db.Atividade.Find(listaExercicios.IdListaExercicio);
                //listaExercicios.numeroListaTurma;listaExercicios.identificadorListaOriginal;listaExercicios.estado;
                if (listaExercicios.nota != null)
                    listaExercicios.estado = 2;//seta lista corrigida

                db.Entry(listaExercicios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdTurma = new SelectList(db.Turma, "IdTurma", "descricao", listaExercicios.IdTurma);
            return View(listaExercicios);
        }

        // GET: ListaExercicios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaExercicios listaExercicios = db.Atividade.Find(id);
            if (listaExercicios == null)
            {
                return HttpNotFound();
            }
            return View(listaExercicios);
        }

        // POST: ListaExercicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ListaExercicios listaExercicios = db.Atividade.Find(id);
            db.Atividade.Remove(listaExercicios);
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
