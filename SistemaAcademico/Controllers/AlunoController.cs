using SistemaAcademico.DAO;
using SistemaAcademico.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static SistemaAcademico.Entidades.Aluno;

namespace SistemaAcademico.Controllers
{
    public class AlunoController : Controller
    {
        // GET: Aluno
        public ActionResult Index()
        {
            AlunoDAO dAO = new AlunoDAO();
            return View(dAO.ListarAlunos());
        }

        //Popula 3 turmas com 20 alunos 
        public ActionResult PopulaAluno()
        {
            //Instanciar DAO
            AlunoDAO dAOAluno = new AlunoDAO();
            TurmaDAO dAOTurma = new TurmaDAO();

            //Gerador de numeros 
            Random random = new Random();

            //Logica para adicionar 20 alunos em cada turma 
            for (int y = 1; y <= 3; y++)
            {
                //Criar uma turma
                Turma turma = new Turma();
                turma.Nome = "Turma " + y;

                //Criar uma lista de alunos
                List<Aluno> ListaDeAlunos = new List<Aluno>();

                for (int x = 1; x <= 20; x++)
                {
                    Aluno aluno = new Aluno();
                    aluno.Nome = "Aluno " + x;
                    aluno.Matricula = (random.Next(1, 10000) + 10000);
                    aluno.Nota1 = random.Next(1, 10);
                    aluno.Nota2 = random.Next(1, 10);
                    aluno.Nota3 = random.Next(1, 10);
                    aluno.Turma = turma;
                    ListaDeAlunos.Add(aluno);
                }
                dAOTurma.Adiciona(turma);
                dAOAluno.Adiciona(ListaDeAlunos);
            }
            return RedirectToAction("Index");
        }

        //Calcular a media de todos os alunos --> Somente notas 1,2 e 3
        public ActionResult CalculaTodasAsMedias()
        {
            AlunoDAO dAO = new AlunoDAO();
            IList<Aluno> alunos =  dAO.ListarAlunos();
            List<Aluno> alunosEditados = new List<Aluno>();
            foreach(Aluno aluno in alunos)
            {
                aluno.CalculaMedia();
                alunosEditados.Add(aluno);
            }
            dAO.Editar(alunosEditados);
            return RedirectToAction("Index");
        }

        //Verificar o Status do Aluno
        public ActionResult VerificaEstado()
        {
            AlunoDAO dAO = new AlunoDAO(); 
            IList<Aluno> alunos = new List<Aluno>(); //Lista para guardar os alunos do banco de dados
            List<Aluno> alunosLista = new List<Aluno>(); //Lista para guardar os alunos que serão editados

            alunos =  dAO.ListarAlunos();
            foreach(Aluno aluno in alunos)
            {
                aluno.VerificaEstado(); //Altera o status do aluno para Aprovado, Reprovado ou Prova Final
                alunosLista.Add(aluno);
            }
            dAO.Editar(alunosLista);
            return RedirectToAction("Index");
        }

        //Popular notas da prova final e calcular as pessoas que ficaram de fato reprovadas. 
        public ActionResult PopulaNotaFinal()
        {
            //Gerador de numeros 
            Random random = new Random();

            AlunoDAO dAO = new AlunoDAO();
            List<Aluno> alunos = new List<Aluno>();
            List<Aluno> alunosLista = new List<Aluno>(); 

            //Buscar alunos com status provaFinal
            alunos = dAO.Busca(_Status.ProvaFinal, 0);
            
            //Gerar notas finais para esses alunos 
            foreach (Aluno aluno in alunos)
            {
                aluno.NotaFinal = random.Next(4,10); //Atribuir nota
                alunosLista.Add(aluno); //Adicionar aluno na lista 
            }
            dAO.Editar(alunosLista);
            return RedirectToAction("Index");
        }

        //Calcula Media das Duas ultimas provas e altera o status do aluno para aprovado/reprovado
        public ActionResult CalcularMediaFinal()
        {
            AlunoDAO dAO = new AlunoDAO();
            IList<Aluno> alunos = new List<Aluno>();
            List<Aluno> alunosLista = new List<Aluno>();

            //Busca por alunos que estão com status de prova final e calcula sua MediaFinal
            alunos = dAO.Busca(_Status.ProvaFinal,0);
            //Logica para calcular a media de cada aluno em prova final 
            foreach(Aluno aluno in alunos)
            {
                aluno.CalculaMediaFinal(); //Calcula e altera o status do aluno para aprovado/reprovado. 
                alunosLista.Add(aluno);
            }
            dAO.Editar(alunosLista);
            return RedirectToAction("Index");
        }

       
    }
}