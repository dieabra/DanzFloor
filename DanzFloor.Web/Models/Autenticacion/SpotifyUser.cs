using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.Autenticacion
{
    /// <summary>
    /// https://developer.spotify.com/web-api/get-current-users-profile/
    /// </summary>
    public class SpotifyUser : ISocialUser
    {
        public string accessToken { get; set; }
        public string email { get; set; }
        public string display_name { get; set; }
        public string id { get; set; }

        public string GetAccessToken()
        {
            return accessToken;
        }

        public string GetEmail()
        {
            return email;
        }

        public string GetId()
        {
            return id;
        }

        public string GetName()
        {
            return display_name;
        }
    }
}