namespace DanzFloor.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MuchosAMuchosBandaArtista : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArtistaBase", "Banda_Id", "dbo.ArtistaBase");
            DropIndex("dbo.ArtistaBase", new[] { "Banda_Id" });
            CreateTable(
                "dbo.BandaArtista",
                c => new
                    {
                        Banda_Id = c.Guid(nullable: false),
                        Artista_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Banda_Id, t.Artista_Id })
                .ForeignKey("dbo.ArtistaBase", t => t.Banda_Id)
                .ForeignKey("dbo.ArtistaBase", t => t.Artista_Id)
                .Index(t => t.Banda_Id)
                .Index(t => t.Artista_Id);
            
            DropColumn("dbo.ArtistaBase", "Banda_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ArtistaBase", "Banda_Id", c => c.Guid());
            DropForeignKey("dbo.BandaArtista", "Artista_Id", "dbo.ArtistaBase");
            DropForeignKey("dbo.BandaArtista", "Banda_Id", "dbo.ArtistaBase");
            DropIndex("dbo.BandaArtista", new[] { "Artista_Id" });
            DropIndex("dbo.BandaArtista", new[] { "Banda_Id" });
            DropTable("dbo.BandaArtista");
            CreateIndex("dbo.ArtistaBase", "Banda_Id");
            AddForeignKey("dbo.ArtistaBase", "Banda_Id", "dbo.ArtistaBase", "Id");
        }
    }
}
