using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.Dominio
{
    public class UsuarioSocial:Entidad
    {
        public string AccessToken { get; set; }
        public string IdSocial { get; set; }
        public string Email { get; set; }
        public string Apellido { get; set; }
        public RedSocial RedSocial { get; set; }
        public string FotoPerfil { get; set; }
    }

    public enum RedSocial
    {
        Google = 0,
        Instagram = 1,
        FaceBook = 2,
        Spotify = 3,
        SoundCloud = 4
    }
}