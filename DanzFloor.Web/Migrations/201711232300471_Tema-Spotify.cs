namespace DanzFloor.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TemaSpotify : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TemaAlbum", "Tema_Id", "dbo.Tema");
            DropForeignKey("dbo.TemaAlbum", "Album_Id", "dbo.Album");
            DropIndex("dbo.TemaAlbum", new[] { "Tema_Id" });
            DropIndex("dbo.TemaAlbum", new[] { "Album_Id" });
            RenameColumn(table: "dbo.Tema", name: "ArtistaBase_Id", newName: "Artista_Id");
            RenameIndex(table: "dbo.Tema", name: "IX_ArtistaBase_Id", newName: "IX_Artista_Id");
            AddColumn("dbo.Tema", "SpotifyLink", c => c.String());
            AddColumn("dbo.Tema", "Album_Id", c => c.Guid());
            CreateIndex("dbo.Tema", "Album_Id");
            AddForeignKey("dbo.Tema", "Album_Id", "dbo.Album", "Id");
            DropTable("dbo.TemaAlbum");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TemaAlbum",
                c => new
                    {
                        Tema_Id = c.Guid(nullable: false),
                        Album_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tema_Id, t.Album_Id });
            
            DropForeignKey("dbo.Tema", "Album_Id", "dbo.Album");
            DropIndex("dbo.Tema", new[] { "Album_Id" });
            DropColumn("dbo.Tema", "Album_Id");
            DropColumn("dbo.Tema", "SpotifyLink");
            RenameIndex(table: "dbo.Tema", name: "IX_Artista_Id", newName: "IX_ArtistaBase_Id");
            RenameColumn(table: "dbo.Tema", name: "Artista_Id", newName: "ArtistaBase_Id");
            CreateIndex("dbo.TemaAlbum", "Album_Id");
            CreateIndex("dbo.TemaAlbum", "Tema_Id");
            AddForeignKey("dbo.TemaAlbum", "Album_Id", "dbo.Album", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TemaAlbum", "Tema_Id", "dbo.Tema", "Id", cascadeDelete: true);
        }
    }
}
