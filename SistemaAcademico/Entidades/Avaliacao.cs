using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaAcademico.Entidades
{
    public class Avaliacao
    {
        [ForeignKey("Aluno")]
        public int Id { get; set; }
        public float Nota1 { get; set; }
        public float Nota2 { get; set; }
        public float Nota3 { get; set; }

        //Calculo
        public float NotaFinal { get; set; }


        public float Media { get; set; }

        //Aluno
        public virtual Aluno Aluno { get; set; }
    }
}