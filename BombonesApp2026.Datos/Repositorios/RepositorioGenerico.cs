using BombonesApp2026.Datos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BombonesApp2026.Datos.Repositorios
{
    public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : class
    {
        protected readonly BombonesDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public RepositorioGenerico(BombonesDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Agregar(T entidad)
        {
            try
            {
                _dbSet.Add(entidad);

            }
            catch (Exception ex)
            {

                throw new Exception($"Error al intentar agregar un registro en la tabla de {typeof(T).Name}",ex);
            } 
        }

        public void Borrar(int id)
        {
            try
            {
                var entidad = _dbSet.Find(id);
                if (entidad is null)
                {
                    throw new KeyNotFoundException($"No se pudo borrar la entidad ID: {id} de la tabla {typeof(T).Name}");
                }
                _dbSet.Remove(entidad);
            }
            catch (Exception ex)
            {

                throw new Exception($"Error al intentar borrar un registro en la tabla de {typeof(T).Name}", ex);
            }
        }

        public void Editar(T entidad, int id)
        {
            try
            {
                var entidadEnDb = _dbSet.Find(id);
                if (entidadEnDb is null) return;
                _dbSet.Entry(entidadEnDb).CurrentValues.SetValues(entidad);
            }
            catch (Exception ex)
            {

                throw new Exception($"Error al intentar editar un registro en la tabla de {typeof(T).Name}", ex);
            }
        }

        public T? ObtenerPorId(int id)
        {
            try
            {
                return _dbSet.Find(id);
            }
            catch (Exception ex)
            {

                throw new Exception($"Error al intentar buscar un registro en la tabla de {typeof(T).Name}", ex);
            }
        }

        public List<T> ObtenerTodos()
        {
            try
            {
                return _dbSet
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception ex)
            {

                throw new Exception($"Error al intentar recuperar los registros en la tabla de {typeof(T).Name}", ex);
            }
        }

        public IQueryable<T> Query()
        {
            return _dbSet.AsNoTracking()
                .AsQueryable();
        }
    }
}
