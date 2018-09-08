using SistemaAcademico.DAO;
using SistemaAcademico.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SistemaAcademico.Controllers
{
    public class PremiosController : Controller
    {
        private static AlunoDAO dAO = new AlunoDAO();
        private static List<Aluno> ListaDeAlunos = new List<Aluno>(); //Lista para salvar alunos editados temporariamente
        private Random random = new Random();


        // GET: Premios
        public ActionResult Index() //Exibe os 5 alunos que irão participar da competição 
        {
            //Listar 5 melhores notas
            AlunoDAO alunoDAO = new AlunoDAO();
            var Lista = dAO.BuscaParametro(10); // Retorna 5 alunos com a media mais alta 
            return View(Lista);
        }

        //Gerar nota para os 5 primeiros alunos 
        public async Task<ActionResult> GerarNotaEspecialAsync()
        {
            //Buscar pelos 5 primeiros alunos com a nota mais alta
            List<Aluno> alunos = new List<Aluno>();
            alunos = dAO.BuscaCompetidores();

            //Preencher a nota desses alunos com uma nata de competição
            foreach (Aluno aluno in alunos)
            {
                aluno.ProvaEspecial = random.Next(1, 10);
                ListaDeAlunos.Add(aluno);
            }

            //Salvo todas as ediçoes feitas 
            await dAO.Editar(ListaDeAlunos);

            //Retornar para o Index
            return RedirectToAction("Index");
        }

        //Calcular notas da competição
        public async Task<ActionResult> CalcularCompeticao()
        {
            var Participantes = dAO.BuscaCompetidores();


            ListaDeAlunos.Clear();
            //Calcular Media de Todas as provas anteriores
            foreach (Aluno aluno in Participantes)
            {
                

                if (aluno.NotaFinal.HasValue) // Se tem valor significa que fez a prova
                {
                    aluno.MediaCompeticao = (aluno.Nota1 + aluno.Nota2 + aluno.Nota3+ Convert.ToDouble(aluno.NotaFinal) + (aluno.ProvaEspecial * 2)) / 6;
                    ListaDeAlunos.Add(aluno);
                }
                else
                {
                    aluno.MediaCompeticao = ((aluno.Nota1 + aluno.Nota2 + aluno.Nota3 + (aluno.ProvaEspecial * 2)) / 5);
                    ListaDeAlunos.Add(aluno);
                }
                await dAO.Editar(ListaDeAlunos);
            }

            return RedirectToAction("Index");
        }
        public ActionResult MostrarRanking() //Exibe o ranking dos alunos que participaram da competição
        {
            var Lista = dAO.BuscaCampeao();
            return View(Lista);
        }

    }
}