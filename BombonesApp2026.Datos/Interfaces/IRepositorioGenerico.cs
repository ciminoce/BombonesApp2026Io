namespace BombonesApp2026.Datos.Interfaces
{
    public interface IRepositorioGenerico<T> where T : class
    {
        List<T> ObtenerTodos();
        IQueryable<T> Query();
        T? ObtenerPorId(int id);
        void Agregar(T entidad);
        void Editar(T entidad, int id);
        void Borrar(int id);
        (List<T> lista, int totalRegistros) ObtenerPagina(int pagina, int cantidad);
    }
}
