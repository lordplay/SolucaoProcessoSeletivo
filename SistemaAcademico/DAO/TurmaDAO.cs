using System;
using SistemaAcademico.DAO;
using SistemaAcademico.Entidades;

namespace SistemaAcademico.Controllers
{
    internal class TurmaDAO
    {
        public void Adiciona(Turma turma)
        {
            using (SistemaContext context = new SistemaContext())
            {
                context.Turmas.Add(turma);
                context.SaveChanges();
            }
        }
    }
}