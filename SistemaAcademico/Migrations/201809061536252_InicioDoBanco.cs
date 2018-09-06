namespace SistemaAcademico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InicioDoBanco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alunoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Matricula = c.Int(nullable: false),
                        Nome = c.String(),
                        Nota1 = c.Single(nullable: false),
                        Nota2 = c.Single(nullable: false),
                        Nota3 = c.Single(nullable: false),
                        TurmaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Turmas", t => t.TurmaId, cascadeDelete: true)
                .Index(t => t.TurmaId);
            
            CreateTable(
                "dbo.Turmas",
                c => new
                    {
                        TurmaId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.TurmaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Alunoes", "TurmaId", "dbo.Turmas");
            DropIndex("dbo.Alunoes", new[] { "TurmaId" });
            DropTable("dbo.Turmas");
            DropTable("dbo.Alunoes");
        }
    }
}
