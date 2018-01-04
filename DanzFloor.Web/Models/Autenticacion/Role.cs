using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models
{
    [Flags]
    public enum RoleEnum
    {
        Administrador,
        Clubber,
        Promotor,
        Colaborador
    }

    public static class RoleConst
    {
        public const string Administrador = "Administrador";

        public const string Clubber = "Clubber";

        public const string Promotor = "Promotor";

        public const string Colaborador = "Colaborador";
    }
}