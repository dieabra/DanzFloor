namespace DanzFloor.Web.Models.Dominio
{
    public class Escenario : Entidad
    {
        public virtual Venue Venue { get; set; }

        public bool Habilitado { get; set; }
    }
}