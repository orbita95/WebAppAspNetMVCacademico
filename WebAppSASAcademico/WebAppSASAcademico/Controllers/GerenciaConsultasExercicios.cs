using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppSASAcademico.Models;

namespace WebAppSASAcademico.Controllers
{
    public class GerenciaConsultasExercicios
    {

        

        public List<ListaExercicios> getListaExerciciosByTurma(int condicao)
        {
            var db = new AcademicoContainer();
            var atividades = db.Atividade.Where(l => l.Turma.condicaoLimiteUm == condicao && l.estado == null).ToList();
            //seleciona apenas as listas criadas pelo professor com base no estado
            db.Dispose();
            return atividades;

        }

        public List<ListaExercicios> getListaExerciciosByAluno(int id)
        {
            var db = new AcademicoContainer();
            var lista = db.Atividade.Where(l => l.idAluno == id).ToList();
            db.Dispose();
            return lista;
        }

        public IEnumerable<Exercicio> getExerciciosByLista(int id)
        {
            var db = new AcademicoContainer();
            var es = db.Exercicio.Where(e => e.IdListaExercicios == id).ToList();
            db.Dispose();
            return es;
        }

        public IEnumerable<Exercicio> getExerciciosListaParaCorrecao(int id)
        {
            var db = new AcademicoContainer();
            var lista = db.Atividade.Find(id);
            var campoIdentificador = lista.IdListaExercicio.ToString() + lista.idAluno.ToString();
            var es = db.Exercicio.Where(e => e.identificador == campoIdentificador).ToList();
            db.Dispose();
            return es;
        }

    }
}