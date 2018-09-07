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
        public double Media { get; set; }


        //Media + prova final
        public double MediaFinal { get; set; }

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
            var _Media = (Nota1 + (Nota2 * 1.2) + (Nota3 * 1.4)) / 3;
            if (_Media > 10)
            {
                this.Media = 10.0;
            }
            else
            {
                this.Media = Math.Round(_Media, 2);
            }
        }

        //Verificar o status do aluno
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

        //Calcula a media final do aluno e atribui aprovado/reprovado
        internal void CalculaMediaFinal()
        {
            //Se a nota for maior que 10, set 10.
            var _MediaFinal = this.Media + this.NotaFinal;
            if (_MediaFinal > 10)
            {
                this.MediaFinal = 10.0;
            }
            else
            {
                this.MediaFinal = Math.Round(_MediaFinal, 2);
            }

            if (this.MediaFinal >= 5)
            {
                this.Status = _Status.Aprovado;
            }
            else
            {
                this.Status = _Status.Reprovado;
            }
        }
    }
}