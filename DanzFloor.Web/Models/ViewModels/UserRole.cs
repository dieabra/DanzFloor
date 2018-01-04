using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.ViewModels
{
    public class UserRole
    {
       
        [Display(Name = "Value")]
        public string Value { get; set; }

        [Display(Name = "Text")]
        public String Text { get; set; }

        [Display(Name = "Text")]
        public bool Selected { get; set; }



    }
}