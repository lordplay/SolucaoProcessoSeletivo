using SistemaAcademico.Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System;
using System.Data.Entity.Migrations;

namespace SistemaAcademico.DAO
{
    public class AlunoDAO
    {
        private SistemaContext contexto = new SistemaContext();

        public void Adiciona(IList<Aluno> alunos)
        {
            IList<Aluno> Lista = alunos;
            using (SistemaContext context = new SistemaContext())
            {
                foreach (Aluno aluno in alunos)
                {
                    context.Alunos.Add(aluno);
                }
                context.SaveChanges();
            }

        }

        //Listar Alunos
        public List<Aluno> ListarAlunos()
        {
            using (SistemaContext context = new SistemaContext())
            {
                return context.Alunos.Include(b => b.Turma).ToList();
            }
        }

        //Editar Aluno
        public async Task Editar(List<Aluno> alunos)
        {
            using (SistemaContext context = new SistemaContext())
            {
                foreach (Aluno aluno in alunos)
                {
                    context.Entry(aluno).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
            }

        }

        //Buscar Academico em Status de Prova Final ---> 
        internal List<Aluno> BuscaEstadoFinal()
        {
            return contexto.Alunos.Include(x => x.Turma).Where(x => x.Status == Aluno._Status.ProvaFinal).ToList();
        }

        internal List<Aluno> BuscaParametro(int? sortBy)
        {
            using (SistemaContext context = new SistemaContext())
            {
                IQueryable<Aluno> alunos = context.Alunos.Include(x => x.Turma);
                if (sortBy != null && sortBy == 0)
                {
                    alunos = alunos.OrderBy(x => x.Nome).ThenBy(x => x.Turma);
                    return alunos.ToList();
                }
                if (sortBy != null && sortBy == 1)
                {
                    alunos = alunos.OrderByDescending(x => x.Status);
                    alunos.ToList();
                }
                if (sortBy != null && sortBy == 2)
                {
                    alunos = alunos.OrderByDescending(x => x.Nota1);
                    alunos.ToList();
                }
                if (sortBy != null && sortBy == 3)
                {
                    alunos = alunos.OrderByDescending(x => x.Nota2);
                    alunos.ToList();
                }
                if (sortBy != null && sortBy == 4)
                {
                    alunos = alunos.OrderByDescending(x => x.Nota3);
                    alunos.ToList();
                }
                if (sortBy != null && sortBy == 5)
                {
                    alunos = alunos.OrderByDescending(x => x.NotaFinal);
                    alunos.ToList();
                }
                if (sortBy != null && sortBy == 6)
                {
                    alunos = alunos.OrderByDescending(x => x.MediaPonderada);
                    alunos.ToList();
                }
                if (sortBy != null && sortBy == 7)
                {
                    alunos = alunos.OrderByDescending(x => x.MediaPonderadaEProvaFinal);
                    alunos.ToList();
                }
                if (sortBy != null && sortBy == 8)
                {
                    alunos = alunos.OrderByDescending(x => x.MediaFinalComTodasAsProvas);
                    alunos.ToList();
                }
                if (sortBy != null && sortBy == 9)
                {
                    alunos = alunos.OrderByDescending(x => x.MediaCompeticao);
                    alunos.ToList();
                }
                if (sortBy != null && sortBy == 10) // Parametro para buscar competidores 
                {
                    alunos = alunos.OrderByDescending(x => x.MediaFinalComTodasAsProvas);
                    return alunos.Take(5).ToList();
                }
                if (sortBy != null && sortBy == 11)
                {
                    alunos = alunos.OrderByDescending(x => x.ProvaEspecial);
                    return alunos.ToList();
                }
                return alunos.ToList(); ;
            }
        }

        //Buscar competidor 
        internal List<Aluno> BuscaCompetidores()
        {

            return contexto.Alunos.Include(x => x.Turma).OrderByDescending(x => x.MediaFinalComTodasAsProvas).Where(X => X.Status == Aluno._Status.Aprovado).Take(5).ToList(); ;
        }


        //Buscar Campeao 
        internal List<Aluno> BuscaCampeao()
        {
            return contexto.Alunos.Include(t => t.Turma).OrderByDescending(x => x.MediaCompeticao).Take(1).ToList();
        }


    }
}