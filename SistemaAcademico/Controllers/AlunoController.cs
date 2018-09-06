using SistemaAcademico.DAO;
using SistemaAcademico.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                    aluno.Avaliacao.Nota1 = random.Next(0, 10);
                    aluno.Avaliacao.Nota2 = random.Next(0, 10);
                    aluno.Avaliacao.Nota3 = random.Next(0, 10);
                    aluno.Turma = turma;
                    ListaDeAlunos.Add(aluno);
                }
                dAOTurma.Adiciona(turma);
                dAOAluno.Adiciona(ListaDeAlunos);
            }
            return RedirectToAction("Index");
        }

        public ActionResult CalculaMedia()
        {

        }
    }
}