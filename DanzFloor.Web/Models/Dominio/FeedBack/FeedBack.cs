using DanzFloor.Web.Models.Dominio.Usuarios;
using System;

namespace DanzFloor.Web.Models.Dominio.FeedBack
{
    /// <summary>
    /// Comentable y Likeable
    /// </summary>
    public class FeedBack:Entidad
    {
        public Guid EntidadId { get; set; }
        public TipoEntidad TipoEntidad { get; set; }

        public TipoFeedBack TipoFeedBack { get; set; }

        public string Comentario { get; set; }
        public virtual Clubber Clubber { get; set; }
    }
}