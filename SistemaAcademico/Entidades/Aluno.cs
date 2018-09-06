using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaAcademico.Entidades
{
    public class Aluno
    {
        //Tabela usuario

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public _Status Status { get; set; }

        //Tabela nota
        public Avaliacao Avaliacao { get; set; }

        //Tabela turma
        public int TurmaId { get; set; }
        public Turma Turma { get; set; }

        //Codigo de Status --> Aprovado ou Nao Aprovado
         public enum _Status
        {
            Reprovado = 0,
            Aprovado = 1
        }
    }
}