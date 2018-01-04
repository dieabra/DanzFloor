using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using DanzFloor.Web.Models.Dominio;
using System.Data.Entity.ModelConfiguration.Conventions;
using DanzFloor.Web.Models.ViewModels;
using DanzFloor.Web.Models.ViewModels.Frontend;
using DanzFloor.Web.Models.Dominio.Usuarios;
using DanzFloor.Web.Models.Dominio.FeedBack;

namespace DanzFloor.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
        }

        public ApplicationUser(RegisterViewModel model)
        {
            UserName = model.Email;
            Email = model.Email;
            Name = model.Name;
            Lastname = model.Lastname;
            TokenFechaVencimiento = DateTime.Now.AddDays(7);
        }

        public ApplicationUser(RegisterFrontViewModel model)
        {
            UserName = model.Email;
            Email = model.Email;
            Name = model.Name;
            Lastname = model.Lastname;
            TokenFechaVencimiento = DateTime.Now.AddDays(7);
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar el Nombre.")]
        [MaxLength(50, ErrorMessage = "El Nombre debe tener como máximo 50 caracteres")]
        public string Name { get; set; }

        [Display(Name = "Apellido")]
        public string Lastname { get; set; }

        [Display(Name = "Token")]
        public Guid Token { get; set; }

        [Display(Name = "Vencimiento del Token")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Column(TypeName = "DateTime2")]
        public DateTime TokenFechaVencimiento { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

        }

        #region Entidades Comunes
        public DbSet<Archivo> Archivo { get; set; }
        public DbSet<ArchivoConfiguracion> ArchivoConfiguracion { get; set; }
        public DbSet<Destacado> Destacado { get; set; }
        public DbSet<GrupoTag> GrupoTag { get; set; }
        public DbSet<Sexo> Sexo { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<VersionMobile> VersionMobile { get; set; }
        #endregion

        #region Usuarios
        public DbSet<Persona> Persona { get; set; }
        //Back
        public DbSet<Colaborador> Colaborador { get; set; }
        //Front
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Clubber> Clubber { get; set; }
        public DbSet<Promotor> Promotor { get; set; }
        #endregion

        public DbSet<ArtistaBase> ArtistaBase { get; set; }
        public DbSet<Artista> Artista { get; set; }
        public DbSet<Tema> Tema { get; set; }
        public DbSet<Album> Album { get; set; }
        public DbSet<SetList> SetList { get; set; }
        public DbSet<Presentacion> Presentacion { get; set; }
        public DbSet<LineUp> LineUp { get; set; }
        public DbSet<Escenario> Escenario { get; set; }
        public DbSet<Venue> Venue { get; set; }
        public DbSet<Fecha> Fecha { get; set; }
        public DbSet<Evento> Evento { get; set; }
        public DbSet<TipoEvento> TipoEvento { get; set; }
        public DbSet<FeedBack> FeedBack { get; set; }
        public DbSet<Banda> Banda { get; set; }

    }
}