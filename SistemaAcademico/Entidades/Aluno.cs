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
        private double? mediaPonderadaEProvaFinal;
        [DisplayName("Média da Avaliação Final")]
        public double? MediaPonderadaEProvaFinal //Definir null caso o usuario nao tenha feito a avaliação final 
        {
            get
            {
                return mediaPonderadaEProvaFinal;
            }
            set
            {
                if (value.HasValue)
                {
                    mediaPonderadaEProvaFinal = Convert.ToDouble(value);
                }
                else
                {
                    mediaPonderadaEProvaFinal = null;
                }

            }
        }

        //Variavel para guardar todas as medias em um lugar
        [DisplayName("Média Final")]
        public double MediaFinalComTodasAsProvas { get; set; }

        //-------------------------------------------Competição------------------------------

        private double? provaEspecial;
        [DisplayName("Avaliação Competitiva")]
        public double? ProvaEspecial
        {
            get
            {
                return provaEspecial;
            }
            set
            {
                if (value.HasValue)
                {
                    provaEspecial = value;
                }
                else
                {
                    provaEspecial = null;
                }
                DefineNotaCampeonato();
            }
        }


        //Media de Todas as notas
        private double? mediaCompeticao;
        [DisplayName("Media da competição")]
        public double? MediaCompeticao
        {
            get
            {
                return mediaCompeticao;
            }
            set
            {
                if (value.HasValue)
                {
                    mediaCompeticao = Math.Round(Convert.ToDouble(value), 2);
                }
                else
                {
                    mediaCompeticao = null;
                }
            }
        }//Caso o aluno não tenha feito a prova, deixar o campo nullo


        //Codigo de Status --> Aprovado - Nao Aprovado - Prova Final
        public enum _Status
        {
            ProvaFinal = 3,
            Reprovado = 0,
            Aprovado = 1
        }


        //Tabela da Turma
        public int TurmaId { get; set; }
        public Turma Turma { get; set; }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------MÉTODOS----------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------

        // Calcula a media do aluno - Provas 1,2 e 3
        public void CalculaMedia()
        {
            var _Media = ((Nota1 + (Nota2 * 1.2) + (Nota3 * 1.4)) / 3.6);
            this.MediaPonderada = Math.Round(_Media, 2);
            PreencheMediaFinalComTodasAsProvas();
        }

        //Verificar o status do aluno - Se está reprovado, aprovado ou em prova final
        public void VerificaEstado() //  
        {
            if (this.MediaPonderada > 6)
            {
                this.Status = _Status.Aprovado;
                PreencheMediaFinalComTodasAsProvas(); // Preenche a nota que será usada para selecionar competidores
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

        //Calcula a Media Ponderada e a Nota Final do aluno, tambem atribui aprovado/reprovado
        internal void CalculaMediaFinal()
        {
            var _MediaFinal = (this.MediaPonderada + this.NotaFinal) / 2;

            this.MediaPonderadaEProvaFinal = Math.Round(Convert.ToDouble(_MediaFinal), 2);

            if (this.MediaPonderadaEProvaFinal >= 5)
            {
                this.Status = _Status.Aprovado;
                PreencheMediaFinalComTodasAsProvas(); // Preenche a nota media final
            }
            else
            {
                this.Status = _Status.Reprovado;
            }

        }

        //Guarda em uma variavel, um valor para usar como comparador de todas as medias tanto as final quanto as normais. 
        public void PreencheMediaFinalComTodasAsProvas()
        {
            if (this.NotaFinal.HasValue)
            {
                this.MediaFinalComTodasAsProvas = Convert.ToDouble(MediaPonderadaEProvaFinal);
            }
            else
            {
                this.MediaFinalComTodasAsProvas = MediaPonderada;
            }
        }

        //Definir Media Campeonato --> Define a Média do aluno que competiu e fez a prova especial.
        public void DefineNotaCampeonato()
        {
            if (NotaFinal.HasValue) // Se tem valor significa que fez a prova
            {
                MediaCompeticao = (this.Nota1 + this.Nota2 + this.Nota3 + Convert.ToDouble(this.NotaFinal) + (Convert.ToDouble(this.ProvaEspecial * 2))) / 6;
            }
            else
            {
                MediaCompeticao = ((Nota1 + Nota2 + Nota3 + (Convert.ToDouble(ProvaEspecial * 2))) / 5);
            }
        }
    }
}