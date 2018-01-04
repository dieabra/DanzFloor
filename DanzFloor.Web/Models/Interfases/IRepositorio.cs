using DanzFloor.Web.Models;
using DanzFloor.Web.Models.Datos;
using System;
using System.Linq;

namespace DanzFloor.Web.Datos
{
    public interface IRepositorio<T> where T : class, IEntidad
    {
        T Traer(Guid Id);
        IQueryable<T> TraerTodos(bool inclusiveEliminados);
        Pagina<T> TraerPagina(int numeroPagina, int elementosPagina, string criterioOrdenamiento, bool ascendente, string criterioBusqueda);
        void Crear(T entidad, bool grabarCambios);
        void Modificar(T entidad, bool grabarCambios);
        void Eliminar(T entidad, bool grabarCambios);
        void GuardarCambios(T entidad);
    }
}