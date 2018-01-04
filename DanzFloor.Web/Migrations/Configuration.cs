namespace DanzFloor.Web.Migrations
{
    using DanzFloor.Web.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<DanzFloor.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DanzFloor.Web.Models.ApplicationDbContext context)
        {
            //Roles
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            foreach (RoleEnum role in Enum.GetValues(typeof(RoleEnum)))
            {
                if (!rm.RoleExists(role.ToString()))
                {
                    var roleResult = rm.Create(new IdentityRole(role.ToString()));
                    if (!roleResult.Succeeded)
                        throw new ApplicationException("Creating role " + role.ToString() + "failed with error(s): " + roleResult.Errors);
                }

            }

            //Users
            var appDBContext = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(appDBContext);
            var userManager = new UserManager<ApplicationUser>(userStore);
            string username = "administrador@binit.com.ar";
            
            if (!(appDBContext.Users.Any(u => u.UserName == username)))
            {
                var userToInsert = new ApplicationUser
                {
                    Name = "Administrador",
                    Lastname = "DanzFloor.Web",
                    UserName = username,
                    PhoneNumber = "+541152356399",
                    Email = username
                };
                userManager.Create(userToInsert, "qweQWE123!#");
            }

            var user = userManager.FindByName(username);
            if (!userManager.IsInRole(user.Id, RoleEnum.Administrador.ToString()))
            {
                var userResult = userManager.AddToRole(user.Id, RoleEnum.Administrador.ToString());
                if (!userResult.Succeeded)
                    throw new ApplicationException("Adding user '" + user.UserName + "' to '" + RoleEnum.Administrador.ToString() + "' role failed with error(s): " + userResult.Errors);
            }

            var db = new ApplicationDbContext();

            var masculino = new Sexo { Id = new Guid("2E5D9308-D47E-9924-7213-39DD9BC99741"), Nombre = "Masculino" };
            var femenino = new Sexo { Id = new Guid("604052FD-F7E6-569B-1776-39DD9BC9980A"), Nombre = "Femenino" };
            db.Sexo.AddOrUpdate(new Sexo[2] {
                masculino,femenino
            });

            db.VersionMobile.AddOrUpdate(new VersionMobile[1] {
                new VersionMobile { Id = new Guid("701D7529-714D-4138-B4A9-3792DFF39A62"),
                    Nombre = "DanzFloor.Web",
                    VersionActual = "1.0",
                    VersionMinima = "1.0",
                    FechaCreacion = DateTime.Now,
                    FechaEdicion = DateTime.Now,
                    Eliminado = false
                }
            });

            db.SaveChanges();

            var appUser = db.Users.First(u => u.UserName == username);
            db.Colaborador.AddOrUpdate(new Models.Dominio.Usuarios.Colaborador
            {
                Id = new Guid("BAB1EC0F-8AA3-4779-914E-678D555AD706"),
                Apellido = appUser.Lastname,
                Celular = appUser.PhoneNumber,
                DNI = 15607365,
                Domicilio = "Aristobulo del Valle 770",
                Email = appUser.Email,
                Localidad = "Caba",
                Nombre = appUser.Name,
                Telefono = appUser.PhoneNumber,
                SexoId = new Guid("604052FD-F7E6-569B-1776-39DD9BC9980A"),
                UsuarioApplicacion = appUser,
                Eliminado = false,
                FechaNacimiento = new DateTime(1943,10,17)
            });

            //var album = new TipoEntidad { Id = new Guid("A01F9D11-5C5B-4FE1-B29A-594FC51330F8"), Nombre = "Album" };
            //var artista = new TipoEntidad { Id = new Guid("0F10C11F-A983-4235-B6B6-76413A854F50"), Nombre = "Artista" };
            //var escenario = new TipoEntidad { Id = new Guid("86AB2C33-CC50-48D5-B009-FC6AE148B287"), Nombre = "Escenario" };
            //var evento = new TipoEntidad { Id = new Guid("E7209E63-3B7A-46D1-964D-E7BED87BD488"), Nombre = "Evento" };
            //var fecha = new TipoEntidad { Id = new Guid("7C857BFE-28A1-4CAB-92AF-157602E12954"), Nombre = "Fecha" };
            //var lineUp = new TipoEntidad { Id = new Guid("A84A9022-93BC-480B-A204-1BEBBDB189E8"), Nombre = "LineUp" };
            //var presentacion = new TipoEntidad { Id = new Guid("C67B0A27-4426-4106-83A6-F4FC89D7F6ED"), Nombre = "Presentacion" };
            //var setList = new TipoEntidad { Id = new Guid("AD516BA4-CDF0-4EDE-A012-CE9B53FF5091"), Nombre = "SetList" };
            //var tema = new TipoEntidad { Id = new Guid("6EC4221D-A2B9-4CE9-BD76-6C76524A5B45"), Nombre = "Tema" };
            //var tipoEvento = new TipoEntidad { Id = new Guid("7FEAE894-14B9-4988-9DCA-CDC2FDD4B577"), Nombre = "TipoEvento" };
            //var venue = new TipoEntidad { Id = new Guid("198FF7A8-7A97-421D-86CE-C5B5B2BB6D6A"), Nombre = "Venue" };
            //var clubber = new TipoEntidad { Id = new Guid("54D96879-46D9-443C-B88A-43561888F9F3"), Nombre = "Clubber" };
            //var promotor = new TipoEntidad { Id = new Guid("28544403-D0F8-4E0E-A3CA-36290E13B694"), Nombre = "Promotor" };
            //var colaborador = new TipoEntidad { Id = new Guid("1D56032B-6D88-49FC-ACC0-4A006CE0C892"), Nombre = "Colaborador" };

            //db.TipoEntidad.AddOrUpdate(new TipoEntidad[14] {
            //    album,artista,escenario,evento,fecha,lineUp,presentacion,setList,tema,tipoEvento,venue,clubber,promotor,colaborador
            //});
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }

            db.ArchivoConfiguracion.AddOrUpdate(new ArchivoConfiguracion[14] {
                // CONFIGURACION ALBUM \\
                new ArchivoConfiguracion
                {
                    Id = new Guid("03567996-CF62-40CD-A32F-12707F07E21C"), Nombre = TipoArchivo.IMAGEN_PRINCIPAL,
                    Extenciones = "", Requerido = true, TamanoMaximo = 2, EsCargaMultiple = false,
                    TipoEntidad = TipoEntidad.album
                },
                // CONFIGURACION ARTISTA \\
                new ArchivoConfiguracion
                {
                    Id = new Guid("9040D79B-5019-413E-8AB6-7835613AC2D8"), Nombre = TipoArchivo.IMAGEN_PRINCIPAL,
                    Extenciones = "", Requerido = true, TamanoMaximo = 2, EsCargaMultiple = false,
                    TipoEntidad = TipoEntidad.artista
                },
                // CONFIGURACION ESCENARIO \\
                new ArchivoConfiguracion
                {
                    Id = new Guid("FFEFD54C-BC91-4FEB-AE5C-63B4B4EBC7A6"), Nombre = TipoArchivo.IMAGEN_PRINCIPAL,
                    Extenciones = "", Requerido = true, TamanoMaximo = 2, EsCargaMultiple = false,
                    TipoEntidad = TipoEntidad.escenario
                },
                // CONFIGURACION EVENTO \\
                new ArchivoConfiguracion
                {
                    Id = new Guid("B2DFF930-C4DE-4FD3-9209-50515FE065CB"), Nombre = TipoArchivo.IMAGEN_PRINCIPAL,
                    Extenciones = "", Requerido = true, TamanoMaximo = 2, EsCargaMultiple = false,
                    TipoEntidad = TipoEntidad.evento
                },
                // CONFIGURACION FECHA \\
                new ArchivoConfiguracion
                {
                    Id = new Guid("F65B9919-925E-4258-9B8B-73DDCAC29F7A"),
                    Nombre = TipoArchivo.ADJUNTOS,
                    Extenciones = "",
                    Requerido = false,
                    TamanoMaximo = 2,
                    EsCargaMultiple = true,
                    TipoEntidad = TipoEntidad.fecha,
                    NoEditable = true
                },
                // CONFIGURACION LINEUP \\
                new ArchivoConfiguracion
                {
                    Id = new Guid("6933C516-F546-4CB8-A60C-11C0EFAEA849"),
                    Nombre = TipoArchivo.IMAGEN_PRINCIPAL,
                    Extenciones = "",
                    Requerido = true,
                    TamanoMaximo = 2,
                    EsCargaMultiple = false,
                    TipoEntidad = TipoEntidad.lineUp
                },
                // CONFIGURACION PRESENTACION \\
                new ArchivoConfiguracion
                {
                    Id = new Guid("05EFE5E2-20FC-4872-A839-2C20DB421929"),
                    Nombre = TipoArchivo.IMAGEN_PRINCIPAL,
                    Extenciones = "",
                    Requerido = true,
                    TamanoMaximo = 2,
                    EsCargaMultiple = false,
                    TipoEntidad = TipoEntidad.presentacion
                },
                // CONFIGURACION SETLIST \\
                new ArchivoConfiguracion
                {
                    Id = new Guid("41E88328-DE50-4489-8C69-6E61995CAC6C"),
                    Nombre = TipoArchivo.IMAGEN_PRINCIPAL,
                    Extenciones = "",
                    Requerido = true,
                    TamanoMaximo = 2,
                    EsCargaMultiple = false,
                    TipoEntidad = TipoEntidad.setList
                },
                // CONFIGURACION TEMA \\
                new ArchivoConfiguracion
                {
                    Id = new Guid("59D5F79B-7E01-4452-8203-E743E7CCCB38"),
                    Nombre = TipoArchivo.IMAGEN_PRINCIPAL,
                    Extenciones = "",
                    Requerido = true,
                    TamanoMaximo = 2,
                    EsCargaMultiple = false,
                    TipoEntidad = TipoEntidad.tema
                },
                // CONFIGURACION TIPO EVENTO \\
                new ArchivoConfiguracion
                {
                    Id = new Guid("BAFB9632-4D0E-459F-B26F-4D27E77A3268"),
                    Nombre = TipoArchivo.IMAGEN_PRINCIPAL,
                    Extenciones = "",
                    Requerido = true,
                    TamanoMaximo = 2,
                    EsCargaMultiple = false,
                    TipoEntidad = TipoEntidad.tipoEvento
                },
                // CONFIGURACION VENUE \\
                new ArchivoConfiguracion
                {
                    Id = new Guid("0942728A-5AD7-4F08-AB3E-8A1DC6808F49"),
                    Nombre = TipoArchivo.IMAGEN_PRINCIPAL,
                    Extenciones = "",
                    Requerido = true,
                    TamanoMaximo = 2,
                    EsCargaMultiple = false,
                    TipoEntidad = TipoEntidad.venue
                },
                // CONFIGURACION CLUBBER \\
                new ArchivoConfiguracion
                {
                    Id = new Guid("FA973503-BAA9-4F5E-89AB-53E82C1397C8"),
                    Nombre = TipoArchivo.IMAGEN_PRINCIPAL,
                    Extenciones = "",
                    Requerido = true,
                    TamanoMaximo = 2,
                    EsCargaMultiple = false,
                    TipoEntidad = TipoEntidad.clubber
                },
                // CONFIGURACION PROMOTOR \\
                new ArchivoConfiguracion
                {
                    Id = new Guid("9B797CB2-E2BB-42CC-9ACD-A8908F178317"),
                    Nombre = TipoArchivo.IMAGEN_PRINCIPAL,
                    Extenciones = "",
                    Requerido = true,
                    TamanoMaximo = 2,
                    EsCargaMultiple = false,
                    TipoEntidad = TipoEntidad.promotor
                },
                // CONFIGURACION COLABORADOR \\
                new ArchivoConfiguracion
                {
                    Id = new Guid("1C85BDC5-DEB0-4C09-9032-4AFF260CED50"),
                    Nombre = TipoArchivo.IMAGEN_PRINCIPAL,
                    Extenciones = "",
                    Requerido = true,
                    TamanoMaximo = 2,
                    EsCargaMultiple = false,
                    TipoEntidad = TipoEntidad.colaborador
                }
            });

            db.SaveChanges();
        }
    }
}
