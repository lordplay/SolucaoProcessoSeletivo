using SistemaAcademico.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SistemaAcademico.DAO
{
    public class SistemaContext : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Nota> Notas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>()
                .HasRequired(s => s.Nota)
                .WithRequiredPrincipal(ad => ad.Aluno);

        }
    }
}