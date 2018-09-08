using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaAcademico.Entidades
{
    public class Turma
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TurmaId { get; set; }
        [DisplayName("Turma")]
        public string Nome { get; set; }

        //Coleção de alunos
        [ForeignKey("TurmaId")]
        public ICollection<Aluno> Alunos { get; set; }
    }
}