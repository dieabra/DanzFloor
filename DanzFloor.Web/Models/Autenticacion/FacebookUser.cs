using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.Autenticacion
{
    public class FacebookUser:ISocialUser
    {
        public string Token { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public string email { get; set; }
        public FBPicture picture { get; set; }

        public string GetAccessToken()
        {
            return Token;
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
            return name;
        }
    }

    public class FBPicture
    {
        public FbPictureData data { get; set; }
    }

    public class FbPictureData
    {
        public bool is_silhouette { get; set; }
        public string url { get; set; }
    }
}