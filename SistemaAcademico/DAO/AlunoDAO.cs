using SistemaAcademico.Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System;
using System.Data.Entity.Migrations;

namespace SistemaAcademico.DAO
{
    public class AlunoDAO : IDisposable
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
        public void Editar(List<Aluno> alunos)
        {
            using (SistemaContext context = new SistemaContext())
            {
                foreach (Aluno aluno in alunos)
                {
                    context.Entry(aluno).State = EntityState.Modified;
                    contexto.SaveChanges();
                }
            }

        }

        //Buscar Academico em Status de Prova Final ---> 
        internal List<Aluno> BuscaEstadoFinal()
        {
            return contexto.Alunos.Include(x => x.Turma).Where(x => x.Status == Aluno._Status.ProvaFinal).ToList();
        }

        //Buscar competidor 
        internal List<Aluno> BuscaCompetidores()
        {
            return contexto.Alunos.Include(x => x.Turma).OrderByDescending(x => x.NotaParaCompeticao).Take(5).ToList();
        }


        //Buscar Campeao 
        internal List<Aluno> BuscaCampeao()
        {
            return contexto.Alunos.Include(t => t.Turma).OrderByDescending(x => x.MediaCompeticao).Take(1).ToList();
        }

        public void Dispose()
        {
            contexto.Dispose();
        }
    }
}