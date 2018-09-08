using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaAcademico.Entidades
{
    public class Aluno
    {

        //Tabela Alunos
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Matrícula")]
        public int Matricula { get; set; }
        [DisplayName("Nome")]
        public string Nome { get; set; }

        [DisplayName("Status")]
        public _Status Status { get; set; }

        //Avaliaçoes
        [DisplayName("Avaliação 1")]
        public double Nota1 { get; set; }
        [DisplayName("Avaliação 2")]
        public double Nota2 { get; set; }
        private double nota3;

        [DisplayName("Avaliação 3")]
        public double Nota3
        {
            get => nota3; set
            {
                nota3 = value;
                this.CalculaMedia();
                this.VerificaEstado();
            }
        }

        //Avaliação Final
        private double? notaFinal;
        [DisplayName("Avaliação Final")]
        public double? NotaFinal
        {
            get
            {
                return notaFinal;
            }
            set
            {
                if (value.HasValue)
                {
                    notaFinal = Convert.ToDouble(value);
                    CalculaMediaFinal();
                }
                else
                {
                    notaFinal = null;
                }

            }
        }


        //Calculo da Media
        [DisplayName("Média")]
        public double MediaPonderada { get; set; }

        //Media + prova final
        [DisplayName("Média da Avaliação Final")]
        public double MediaPonderadaEProvaFinal { get; set; }

        //Variavel para guardar a nota final para entrar na competição
        [DisplayName("Nota Competitiva")]
        public double NotaParaCompeticao { get; set; }

        //-------------------------------------------Competição----------------------
        //Prova Especial
        [DisplayName("Avaliação Competitiva")]
        public double ProvaEspecial { get; set; }

        //Media de Todas as notas
        private double mediaCompeticao;
        [DisplayName("Media Competitiva")]
        public double MediaCompeticao { get => mediaCompeticao ; set => mediaCompeticao = Math.Round(value, 2); }
        //-----------------------------------------------------------------------------
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
            var _Media = ((Nota1 + (Nota2 * 1.2) + (Nota3 * 1.4)) / 3.6);
            this.MediaPonderada = Math.Round(_Media, 2);
        }

        //Verificar o status do aluno
        public void VerificaEstado() //  
        {
            if (this.MediaPonderada > 6)
            {
                this.Status = _Status.Aprovado;
            }
            else if (this.MediaPonderada < 4)
            {
                this.Status = _Status.Reprovado;
            }
            else
            {
                this.Status = _Status.ProvaFinal;
            }
        }

        //Calcula a Media Ponderada e Nota Final do aluno e atribui aprovado/reprovado
        internal void CalculaMediaFinal()
        {
            var _MediaFinal = (this.MediaPonderada + this.NotaFinal) / 2;

            this.MediaPonderadaEProvaFinal = Math.Round(Convert.ToDouble(_MediaFinal), 2);

            if (this.MediaPonderadaEProvaFinal >= 5)
            {
                this.Status = _Status.Aprovado;
            }
            else
            {
                this.Status = _Status.Reprovado;
            }
        }

        //Preencher minha NotaParaCompeticao
        public void PreencheNotaParaCompeticao()
        {
            if (this.NotaFinal.HasValue)
            {
                this.NotaParaCompeticao = MediaPonderadaEProvaFinal;
            }
            else
            {
                this.NotaParaCompeticao = MediaPonderada;
            }
        }


    }
}