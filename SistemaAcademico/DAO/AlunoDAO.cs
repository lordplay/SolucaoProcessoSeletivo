using SistemaAcademico.Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System;

namespace SistemaAcademico.DAO
{
    public class AlunoDAO
    {
        public void Adiciona(IList<Aluno> alunos)
        {
            //Recebe uam lista de alunos e para cada aluno, ele adiciona ao banco de dados --> 
            using (SistemaContext context = new SistemaContext())
            {
                IList<Aluno> Lista = alunos;
                foreach (Aluno aluno in alunos)
                {
                    context.Alunos.Add(aluno);
                }
                context.SaveChanges();
            }
        }

        //Listar Alunos
        public  IList<Aluno> ListarAlunos()
        {
            using (SistemaContext context = new SistemaContext())
            {
                return context.Alunos.Include(b => b.Turma).ToList();
            }
        }

        //Editar Aluno
        public void Editar(IList<Aluno> alunos)
        {
            using (SistemaContext context = new SistemaContext())
            {
                foreach (Aluno aluno in alunos)
                {
                    context.Entry(aluno).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }

        //Buscar Academico em Status de Prova Final ---> 
        internal List<Aluno> Busca(Aluno._Status? provaFinal)
        {
            using (SistemaContext context = new SistemaContext())
            {
                IQueryable<Aluno> busca = context.Alunos;
                if ((int)provaFinal != 4) // Sempre verdadeiro 
                {
                    busca = busca.Where(p => p.Status == provaFinal); //Filtra o resultado somente para alunos que irão realizar a prova final. 
                }
                return busca.ToList();
            }
        }
    }
}