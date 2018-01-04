using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.Dominio
{
    public abstract class TagBase: Entidad
    {
        public TagBase()
        {
            GrupoTags = new List<GrupoTag>();
        }
        

        [Display(Name = "Productos")]
        public virtual ICollection<GrupoTag> GrupoTags { get; set; }
    }
}