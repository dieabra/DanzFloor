namespace DanzFloor.Web.Models.Autenticacion
{
    public class InstagramUser:ISocialUser
    {
        public string Token { get; set; }
        public string id { get; set; }
        public string username { get; set; }
        public string full_name { get; set; }
        public string profile_picture { get; set; }

        public string GetAccessToken()
        {
            return Token;
        }

        public string GetEmail()
        {
            return "";
        }

        public string GetId()
        {
            return id;
        }

        public string GetName()
        {
            return full_name;
        }
    }
}