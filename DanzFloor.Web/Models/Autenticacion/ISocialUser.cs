using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.Autenticacion
{
    public interface ISocialUser
    {
        string GetName();
        string GetId();
        string GetEmail();
        string GetAccessToken();
    }
}