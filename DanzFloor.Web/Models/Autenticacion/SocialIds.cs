using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.Autenticacion
{
    public static class SocialIds
    {
        public static string GoogleClientId { get { return "798083776747-irfipedjps0m8ghr3qdt2u9fo8npc74o.apps.googleusercontent.com"; } }
        public static string InstagramClientId { get { return "766809c2e8a743e2be0d77d1b97c959b"; } }
        public static string InstagramClientSecret { get { return "21a1e336cfc44f1aa83adca316320f58"; } }
        public static string InstagramRedirectUri { get { return "https://api.ionic.io/auth/integrations/instagram"; } }

        public static string InstagramRedirectUriWeb { get { return "https://localhost:44321/FrontEnd/ReturnFromInstagram"; } }
        public static string InstagramGrantType { get { return "authorization_code"; } }

        public static string FacebookSecret { get { return "74e58b921cb0baa626ae6682bfea2d4a"; } }
        public static string FacebookAppId { get { return "555576394787302"; } }
        public static string FacebookAppName { get { return "Login"; } }

        public static string SpotifyClientId { get { return "5586c87f452242d68291aea20e6ca28e"; } }
        public static string SpotifySecret { get { return "89b1b5b76b7e406ba631d389057110d7"; } }
    }
}