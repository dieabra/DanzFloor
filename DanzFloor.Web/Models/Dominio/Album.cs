using DanzFloor.Web.Datos;
using DanzFloor.Web.Models.Dominio.FeedBack;
using DanzFloor.Web.Models.Dominio.Usuarios;
using System.Collections.Generic;

namespace DanzFloor.Web.Models.Dominio
{
    public class Album:EntidadConArchivo
    {
        public virtual List<Tema> Temas { get; set; }
        public int Likes { get; set; }

        public void Like(ApplicationDbContext context)
        {
            var actual = Helpers.UsuarioClienteHelper.Actual(context);
            if (actual is Clubber)
            {
                new Repositorio<FeedBack.FeedBack>(context).Crear(new FeedBack.FeedBack() { Clubber = actual as Clubber, TipoEntidad = TipoEntidad.album, TipoFeedBack = TipoFeedBack.Like},false);
                var repo = new Repositorio<Album>(context);
                var get = repo.Traer(this.Id);
                get.Likes++;
                repo.Modificar(get);
            }
            else
                throw new System.Exception("Solo pueden Likear Clubbers");
        }
    }
}