namespace DanzFloor.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Album",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Likes = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Archivo",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Titulo = c.String(maxLength: 200),
                        Descripcion = c.String(maxLength: 500),
                        Contenido = c.String(),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                        ArchivoConfiguracion_Id = c.Guid(),
                        Album_Id = c.Guid(),
                        Presentacion_Id = c.Guid(),
                        ArtistaBase_Id = c.Guid(),
                        Destacado_Id = c.Guid(),
                        Venue_Id = c.Guid(),
                        Evento_Id = c.Guid(),
                        Fecha_Id = c.Guid(),
                        LineUp_Id = c.Guid(),
                        Persona_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ArchivoConfiguracion", t => t.ArchivoConfiguracion_Id)
                .ForeignKey("dbo.Album", t => t.Album_Id)
                .ForeignKey("dbo.Presentacion", t => t.Presentacion_Id)
                .ForeignKey("dbo.ArtistaBase", t => t.ArtistaBase_Id)
                .ForeignKey("dbo.Destacado", t => t.Destacado_Id)
                .ForeignKey("dbo.Venue", t => t.Venue_Id)
                .ForeignKey("dbo.Evento", t => t.Evento_Id)
                .ForeignKey("dbo.Fecha", t => t.Fecha_Id)
                .ForeignKey("dbo.LineUp", t => t.LineUp_Id)
                .ForeignKey("dbo.Persona", t => t.Persona_Id)
                .Index(t => t.ArchivoConfiguracion_Id)
                .Index(t => t.Album_Id)
                .Index(t => t.Presentacion_Id)
                .Index(t => t.ArtistaBase_Id)
                .Index(t => t.Destacado_Id)
                .Index(t => t.Venue_Id)
                .Index(t => t.Evento_Id)
                .Index(t => t.Fecha_Id)
                .Index(t => t.LineUp_Id)
                .Index(t => t.Persona_Id);
            
            CreateTable(
                "dbo.ArchivoConfiguracion",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TipoEntidad = c.Int(nullable: false),
                        Extenciones = c.String(),
                        Requerido = c.Boolean(nullable: false),
                        TamanoMaximo = c.Int(nullable: false),
                        EsCargaMultiple = c.Boolean(nullable: false),
                        NoEditable = c.Boolean(nullable: false),
                        HabilitaTagsCliente = c.Boolean(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tema",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                        SetList_Id = c.Guid(),
                        ArtistaBase_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SetList", t => t.SetList_Id)
                .ForeignKey("dbo.ArtistaBase", t => t.ArtistaBase_Id)
                .Index(t => t.SetList_Id)
                .Index(t => t.ArtistaBase_Id);
            
            CreateTable(
                "dbo.ArtistaBase",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Presentacion_Id = c.Guid(),
                        Presentacion_Id1 = c.Guid(),
                        Banda_Id = c.Guid(),
                        Usuario_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Presentacion", t => t.Presentacion_Id)
                .ForeignKey("dbo.Presentacion", t => t.Presentacion_Id1)
                .ForeignKey("dbo.ArtistaBase", t => t.Banda_Id)
                .ForeignKey("dbo.Persona", t => t.Usuario_Id)
                .Index(t => t.Presentacion_Id)
                .Index(t => t.Presentacion_Id1)
                .Index(t => t.Banda_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.Presentacion",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Inicio = c.DateTime(nullable: false),
                        Fin = c.DateTime(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                        SetList_Id = c.Guid(),
                        ArtistaBase_Id = c.Guid(),
                        LineUp_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SetList", t => t.SetList_Id)
                .ForeignKey("dbo.ArtistaBase", t => t.ArtistaBase_Id)
                .ForeignKey("dbo.LineUp", t => t.LineUp_Id)
                .Index(t => t.SetList_Id)
                .Index(t => t.ArtistaBase_Id)
                .Index(t => t.LineUp_Id);
            
            CreateTable(
                "dbo.SetList",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Persona",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Apellido = c.String(),
                        DNI = c.Int(nullable: false),
                        Domicilio = c.String(maxLength: 100),
                        Localidad = c.String(maxLength: 100),
                        Telefono = c.String(),
                        Celular = c.String(maxLength: 20),
                        FechaNacimiento = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 200),
                        SexoId = c.Guid(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        UsuarioApplicacion_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sexo", t => t.SexoId)
                .ForeignKey("dbo.ApplicationUser", t => t.UsuarioApplicacion_Id)
                .Index(t => t.SexoId)
                .Index(t => t.UsuarioApplicacion_Id);
            
            CreateTable(
                "dbo.Sexo",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 50),
                        Lastname = c.String(),
                        Token = c.Guid(nullable: false),
                        TokenFechaVencimiento = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.UsuarioSocial",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IdSocial = c.String(),
                        Email = c.String(),
                        Apellido = c.String(),
                        RedSocial = c.Int(nullable: false),
                        FotoPerfil = c.String(),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                        Clubber_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Persona", t => t.Clubber_Id)
                .Index(t => t.Clubber_Id);
            
            CreateTable(
                "dbo.Destacado",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Titulo = c.String(),
                        Descripcion = c.String(),
                        FechaPublicacion = c.DateTime(nullable: false),
                        Habilitado = c.Boolean(nullable: false),
                        Link = c.String(),
                        NombreLink = c.String(),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Escenario",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Habilitado = c.Boolean(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                        Venue_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Venue", t => t.Venue_Id)
                .Index(t => t.Venue_Id);
            
            CreateTable(
                "dbo.Venue",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Direccion = c.String(),
                        Localidad = c.String(),
                        Latitud = c.String(),
                        Longitud = c.String(),
                        Habilitado = c.Boolean(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Evento",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                        TipoEvento_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipoEvento", t => t.TipoEvento_Id)
                .Index(t => t.TipoEvento_Id);
            
            CreateTable(
                "dbo.Fecha",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Dia = c.DateTime(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                        Evento_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Evento", t => t.Evento_Id)
                .Index(t => t.Evento_Id);
            
            CreateTable(
                "dbo.LineUp",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                        Escenario_Id = c.Guid(),
                        Fecha_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Escenario", t => t.Escenario_Id)
                .ForeignKey("dbo.Fecha", t => t.Fecha_Id)
                .Index(t => t.Escenario_Id)
                .Index(t => t.Fecha_Id);
            
            CreateTable(
                "dbo.TipoEvento",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FeedBack",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EntidadId = c.Guid(nullable: false),
                        TipoEntidad = c.Int(nullable: false),
                        TipoFeedBack = c.Int(nullable: false),
                        Comentario = c.String(),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                        Clubber_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Persona", t => t.Clubber_Id)
                .Index(t => t.Clubber_Id);
            
            CreateTable(
                "dbo.GrupoTag",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisibleFront = c.Boolean(nullable: false),
                        EsCaracteristica = c.Boolean(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisibleFront = c.Boolean(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VersionMobile",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VersionMinima = c.String(nullable: false),
                        VersionActual = c.String(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TemaAlbum",
                c => new
                    {
                        Tema_Id = c.Guid(nullable: false),
                        Album_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tema_Id, t.Album_Id })
                .ForeignKey("dbo.Tema", t => t.Tema_Id, cascadeDelete: true)
                .ForeignKey("dbo.Album", t => t.Album_Id, cascadeDelete: true)
                .Index(t => t.Tema_Id)
                .Index(t => t.Album_Id);
            
            CreateTable(
                "dbo.TagGrupoTag",
                c => new
                    {
                        Tag_Id = c.Guid(nullable: false),
                        GrupoTag_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.GrupoTag_Id })
                .ForeignKey("dbo.Tag", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.GrupoTag", t => t.GrupoTag_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.GrupoTag_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Persona", "UsuarioApplicacion_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.Persona", "SexoId", "dbo.Sexo");
            DropForeignKey("dbo.Archivo", "Persona_Id", "dbo.Persona");
            DropForeignKey("dbo.TagGrupoTag", "GrupoTag_Id", "dbo.GrupoTag");
            DropForeignKey("dbo.TagGrupoTag", "Tag_Id", "dbo.Tag");
            DropForeignKey("dbo.FeedBack", "Clubber_Id", "dbo.Persona");
            DropForeignKey("dbo.Evento", "TipoEvento_Id", "dbo.TipoEvento");
            DropForeignKey("dbo.Fecha", "Evento_Id", "dbo.Evento");
            DropForeignKey("dbo.LineUp", "Fecha_Id", "dbo.Fecha");
            DropForeignKey("dbo.Presentacion", "LineUp_Id", "dbo.LineUp");
            DropForeignKey("dbo.LineUp", "Escenario_Id", "dbo.Escenario");
            DropForeignKey("dbo.Archivo", "LineUp_Id", "dbo.LineUp");
            DropForeignKey("dbo.Archivo", "Fecha_Id", "dbo.Fecha");
            DropForeignKey("dbo.Archivo", "Evento_Id", "dbo.Evento");
            DropForeignKey("dbo.Escenario", "Venue_Id", "dbo.Venue");
            DropForeignKey("dbo.Archivo", "Venue_Id", "dbo.Venue");
            DropForeignKey("dbo.Archivo", "Destacado_Id", "dbo.Destacado");
            DropForeignKey("dbo.ArtistaBase", "Usuario_Id", "dbo.Persona");
            DropForeignKey("dbo.Tema", "ArtistaBase_Id", "dbo.ArtistaBase");
            DropForeignKey("dbo.Presentacion", "ArtistaBase_Id", "dbo.ArtistaBase");
            DropForeignKey("dbo.Archivo", "ArtistaBase_Id", "dbo.ArtistaBase");
            DropForeignKey("dbo.ArtistaBase", "Banda_Id", "dbo.ArtistaBase");
            DropForeignKey("dbo.UsuarioSocial", "Clubber_Id", "dbo.Persona");
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.Presentacion", "SetList_Id", "dbo.SetList");
            DropForeignKey("dbo.Tema", "SetList_Id", "dbo.SetList");
            DropForeignKey("dbo.ArtistaBase", "Presentacion_Id1", "dbo.Presentacion");
            DropForeignKey("dbo.ArtistaBase", "Presentacion_Id", "dbo.Presentacion");
            DropForeignKey("dbo.Archivo", "Presentacion_Id", "dbo.Presentacion");
            DropForeignKey("dbo.TemaAlbum", "Album_Id", "dbo.Album");
            DropForeignKey("dbo.TemaAlbum", "Tema_Id", "dbo.Tema");
            DropForeignKey("dbo.Archivo", "Album_Id", "dbo.Album");
            DropForeignKey("dbo.Archivo", "ArchivoConfiguracion_Id", "dbo.ArchivoConfiguracion");
            DropIndex("dbo.TagGrupoTag", new[] { "GrupoTag_Id" });
            DropIndex("dbo.TagGrupoTag", new[] { "Tag_Id" });
            DropIndex("dbo.TemaAlbum", new[] { "Album_Id" });
            DropIndex("dbo.TemaAlbum", new[] { "Tema_Id" });
            DropIndex("dbo.FeedBack", new[] { "Clubber_Id" });
            DropIndex("dbo.LineUp", new[] { "Fecha_Id" });
            DropIndex("dbo.LineUp", new[] { "Escenario_Id" });
            DropIndex("dbo.Fecha", new[] { "Evento_Id" });
            DropIndex("dbo.Evento", new[] { "TipoEvento_Id" });
            DropIndex("dbo.Escenario", new[] { "Venue_Id" });
            DropIndex("dbo.UsuarioSocial", new[] { "Clubber_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Persona", new[] { "UsuarioApplicacion_Id" });
            DropIndex("dbo.Persona", new[] { "SexoId" });
            DropIndex("dbo.Presentacion", new[] { "LineUp_Id" });
            DropIndex("dbo.Presentacion", new[] { "ArtistaBase_Id" });
            DropIndex("dbo.Presentacion", new[] { "SetList_Id" });
            DropIndex("dbo.ArtistaBase", new[] { "Usuario_Id" });
            DropIndex("dbo.ArtistaBase", new[] { "Banda_Id" });
            DropIndex("dbo.ArtistaBase", new[] { "Presentacion_Id1" });
            DropIndex("dbo.ArtistaBase", new[] { "Presentacion_Id" });
            DropIndex("dbo.Tema", new[] { "ArtistaBase_Id" });
            DropIndex("dbo.Tema", new[] { "SetList_Id" });
            DropIndex("dbo.Archivo", new[] { "Persona_Id" });
            DropIndex("dbo.Archivo", new[] { "LineUp_Id" });
            DropIndex("dbo.Archivo", new[] { "Fecha_Id" });
            DropIndex("dbo.Archivo", new[] { "Evento_Id" });
            DropIndex("dbo.Archivo", new[] { "Venue_Id" });
            DropIndex("dbo.Archivo", new[] { "Destacado_Id" });
            DropIndex("dbo.Archivo", new[] { "ArtistaBase_Id" });
            DropIndex("dbo.Archivo", new[] { "Presentacion_Id" });
            DropIndex("dbo.Archivo", new[] { "Album_Id" });
            DropIndex("dbo.Archivo", new[] { "ArchivoConfiguracion_Id" });
            DropTable("dbo.TagGrupoTag");
            DropTable("dbo.TemaAlbum");
            DropTable("dbo.VersionMobile");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Tag");
            DropTable("dbo.GrupoTag");
            DropTable("dbo.FeedBack");
            DropTable("dbo.TipoEvento");
            DropTable("dbo.LineUp");
            DropTable("dbo.Fecha");
            DropTable("dbo.Evento");
            DropTable("dbo.Venue");
            DropTable("dbo.Escenario");
            DropTable("dbo.Destacado");
            DropTable("dbo.UsuarioSocial");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.Sexo");
            DropTable("dbo.Persona");
            DropTable("dbo.SetList");
            DropTable("dbo.Presentacion");
            DropTable("dbo.ArtistaBase");
            DropTable("dbo.Tema");
            DropTable("dbo.ArchivoConfiguracion");
            DropTable("dbo.Archivo");
            DropTable("dbo.Album");
        }
    }
}
