//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAppSASAcademico.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ListaExercicios
    {
        public int IdListaExercicio { get; set; }
        public Nullable<double> nota { get; set; }
        public string descricao { get; set; }
        public string nomeAluno { get; set; }
        public Nullable<int> idAluno { get; set; }
        public int IdTurma { get; set; }
        public Nullable<int> estado { get; set; }
        public Nullable<int> identificadorListaOriginal { get; set; }
        public Nullable<int> numeroListaTurma { get; set; }
    
        public virtual Turma Turma { get; set; }
    }
}
