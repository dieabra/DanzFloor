using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.Dominio
{
    public class Tag: TagBase
    {
        public Tag() : base()
        {
        }

        [Display(Name = "Visible en frontend")]
        public bool VisibleFront { get; set; }
    }
}