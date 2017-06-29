using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppSASAcademico.Models;

namespace WebAppSASAcademico.Controllers
{
    public class AlunosController : Controller
    {
        private GerenciaApiAlunos mgr = new GerenciaApiAlunos();
        private AcademicoContainer db = new AcademicoContainer();
        private GerenciaConsultasExercicios gce = new GerenciaConsultasExercicios();
        

        // GET: Alunos
        public ActionResult Index()
        {
           return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            try
            {
                int id = Convert.ToInt32(form["cpf"]);
                if (id > 0)
                {
                    var a = mgr.GetAlunoByCpf(id);

                    TempData["Aluno"] = a;

                    return View("Details", a);
                }

                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
            
            
        }
                
        //retorna listas de exercicios de um aluno
        public ActionResult ListaExercicios(string data, int id)
        {
            var anoNascimento = Convert.ToDateTime(data).Year;//condição de formação das turmas
            

            var a = TempData["Aluno"] as Alunos;
            
                        
            var listas = gce.getListaExerciciosByAluno(a.id);//listas assinadas com registro do aluno
            var listasOriginais = gce.getListaExerciciosByTurma(anoNascimento);//turmas são formadas com base na data de nascimento

            var diferenca = listasOriginais.Count - listas.Count;

            if (diferenca > 0)// se verdadeiro seleciona as originais do professor e assina com registro do aluno
            {
                var max = listasOriginais.Count;
                var maxAux = listas.Count; ;


                for (; max > maxAux; max--)
                {
                    var item = listasOriginais.ElementAt(max - 1);
                    // assinatura do aluno na lista de exercicios
                    
                    item.idAluno = a.id;
                    item.nomeAluno = a.nome;
                    item.estado = 0;// salvo/assinado pelo aluno

                    db.Atividade.Add(item);
                    //cria uma nova lista pertencente ao aluno
                }

                db.SaveChanges();
                listas = gce.getListaExerciciosByAluno(a.id);
                
            }

            if (listas.Count == 0)
                return View(listasOriginais);
            else
                return View(listas);

        }
        
        //recupera os exercicios de uma lista para o aluno
        public ActionResult ExerciciosLista(int id, int idAluno, int num)
        {
            var exercicios = gce.getExerciciosByLista(id);//exercicios de uma lista 
            var lista = db.Atividade.Where(l => l.idAluno == idAluno && l.numeroListaTurma == num).First();

            TempData["ListaExercicioAtual"] = lista;
            
            return View(exercicios);
        }

        public ActionResult EditExerciciosAluno(int id)
        {
            //respostaAluno
            Exercicio exercicio = db.Exercicio.Find(id);
            if (exercicio == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdListaExercicios = new SelectList(db.Atividade, "IdListaExercicio", "descricao", exercicio.IdListaExercicios);
                        
            return View(exercicio);
            
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EditExerciciosAluno([Bind(Include = "IdExercicio,enunciado,respostaAluno,respostaProfessor,comentario,IdListaExercicios")] Exercicio exercicio)
        {
            
            if (ModelState.IsValid)
            {
                if(exercicio.respostaAluno != null)
                {
                    var auxExercicio = db.Exercicio.Find(exercicio.IdExercicio);//recupera os dados do exercicio base do professor

                    var auxLista = TempData["ListaExercicioAtual"] as ListaExercicios;

                    auxExercicio.respostaAluno = exercicio.respostaAluno;
                    auxExercicio.identificador = auxLista.IdListaExercicio.ToString() + auxLista.idAluno.ToString();

                    db.Exercicio.Add(auxExercicio);
                    db.SaveChanges();
                }
                
                return RedirectToAction("Index");
            }
            ViewBag.IdListaExercicios = new SelectList(db.Atividade, "IdListaExercicio", "descricao", exercicio.IdListaExercicios);
            return View(exercicio);
        }

        public ActionResult SalvarExerciciosLista(int id)
        {
            try
            {
                var listaEs = db.Atividade.Find(id);

                listaEs.estado = 1;// estado 1 enviado para correção
                db.Entry(listaEs).State = EntityState.Modified;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                Console.WriteLine( ex.Message);
                return RedirectToAction("Index");
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
