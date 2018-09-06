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
        public int Id { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public float Nota1{ get; set; }
        public float Nota2 { get; set; }
        public float Nota3 { get; set; }

        //Tabela turma
        public int TurmaId { get; set; }
        public Turma Turma { get; set; }
        
    }
}