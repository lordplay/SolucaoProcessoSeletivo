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
        private double _media;

        //Tabela usuario

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public _Status Status { get; set; }

        //Tabela nota
        public double Nota1 { get; set; }
        public double Nota2 { get; set; }
        public double Nota3 { get; set; }

        //Calculo NotaFinal
        public double NotaFinal { get; set; }

        //Calculo Media
        public double Media { get => Math.Round(_media, 2); set => _media = value; } //Limita o valor dos 0 

        //Tabela turma
        public int TurmaId { get; set; }
        public Turma Turma { get; set; }

        //Codigo de Status --> Aprovado ou Nao Aprovado
        public enum _Status
        {
            ProvaFinal = 3,
            Reprovado = 0,
            Aprovado = 1
        }

        // N2 = 20% || N3 = 40% 
        public void CalculaMedia()
        {
            this.Media = (Nota1 + (Nota2 * 0.2) + (Nota3 * 0.4)) / 3;
        }

        public void VerificaEstado() //  
        {
            if (this.Media > 6)
            {
                this.Status = _Status.Aprovado;
            }
            else if (this.Media < 4)
            {
                this.Status = _Status.Reprovado;
            }
            else
            {
                this.Status = _Status.ProvaFinal;
            }
        }
    }
}