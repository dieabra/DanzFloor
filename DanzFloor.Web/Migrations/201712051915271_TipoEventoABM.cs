namespace DanzFloor.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TipoEventoABM : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Evento", new[] { "TipoEvento_Id" });
            AddColumn("dbo.UsuarioSocial", "AccessToken", c => c.String());
            AddColumn("dbo.Evento", "Venue_Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Evento", "TipoEvento_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.Evento", "TipoEvento_Id");
            CreateIndex("dbo.Evento", "Venue_Id");
            AddForeignKey("dbo.Evento", "Venue_Id", "dbo.Venue", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Evento", "Venue_Id", "dbo.Venue");
            DropIndex("dbo.Evento", new[] { "Venue_Id" });
            DropIndex("dbo.Evento", new[] { "TipoEvento_Id" });
            AlterColumn("dbo.Evento", "TipoEvento_Id", c => c.Guid());
            DropColumn("dbo.Evento", "Venue_Id");
            DropColumn("dbo.UsuarioSocial", "AccessToken");
            CreateIndex("dbo.Evento", "TipoEvento_Id");
        }
    }
}
