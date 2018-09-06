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

        //Tabela nota
        public Nota Nota { get; set; }

        //Tabela turma
        public int TurmaId { get; set; }
        public Turma Turma { get; set; }
        
    }
}