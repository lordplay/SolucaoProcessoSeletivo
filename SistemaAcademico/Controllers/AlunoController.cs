using SistemaAcademico.DAO;
using SistemaAcademico.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
        
        public ActionResult Index(int? sortOrder)
        {
            AlunoDAO dAO = new AlunoDAO();

   

            return View(dAO.BuscaParametro(sortOrder));

        }

        //Popula 3 turmas com 20 alunos -
        public ActionResult PopulaAluno()
        {
            //Instanciar DAO
            TurmaDAO dAOTurma = new TurmaDAO();
            AlunoDAO dAOAluno = new AlunoDAO();

            //Gerador de numeros 
            Random random = new Random();


            //Logica para adicionar 20 alunos em cada turma 

            using (SistemaContext context = new SistemaContext())
            {
                for (int y = 1; y <= 3; y++)
                {
                    //Criar uma turma
                    Turma turma = new Turma();
                    turma.Nome = "Turma " + y;
                    //Criar uma Lista de Alunos 
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

            }
            return RedirectToAction("Index");
        }

        //Popular notas da prova final e calcular as pessoas que ficaram de fato reprovadas. 
        public async Task<ActionResult> PopulaNotaFinalAsync()
        {
            //Instancia do DAO
            AlunoDAO dAO = new AlunoDAO();

            //Gerador de numeros 
            Random random = new Random();

            //Instancia de listas
            List<Aluno> alunos = new List<Aluno>();
            List<Aluno> ListaDeAlunos = new List<Aluno>(); //Lista para guardar os alunos a serem modificados 
            //Buscar alunos com status provaFinal
            alunos = dAO.BuscaEstadoFinal();

            //Gerar notas finais para esses alunos 
            foreach (Aluno aluno in alunos)
            {
                aluno.NotaFinal = random.Next(4, 10); //Atribuir nota
                ListaDeAlunos.Add(aluno); //Adicionar aluno na lista 
            }
            await dAO.Editar(ListaDeAlunos);
            return RedirectToAction("Index");
        }


        public ActionResult Delete()
        {
            using (SistemaContext c = new SistemaContext())
            {
                c.Turmas.RemoveRange(c.Turmas);
                c.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}