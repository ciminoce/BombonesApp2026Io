using BombonesApp2026.Datos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

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
            _dbSet.Add(entidad);
        }
        public virtual void Borrar(int id)
        {
            var entidad = _dbSet.Find(id);
            if (entidad is null)
            {
                throw new KeyNotFoundException($"No se pudo borrar la entidad ID: {id} de la tabla {typeof(T).Name}");
            }
            _dbSet.Remove(entidad);
        }

        public void Editar(T entidad, int id)
        {
            var entidadEnDb = _dbSet.Find(id);
            if (entidadEnDb is null)
            {
                throw new KeyNotFoundException($"No se pudo borrar la entidad ID: {id} de la tabla {typeof(T).Name}");
            }
            _dbSet.Entry(entidadEnDb).CurrentValues.SetValues(entidad);
        }

        public (List<T> lista, int totalRegistros) ObtenerPagina(int pagina, 
            int cantidad, Func<IQueryable<T>,IOrderedQueryable<T>> ordenarPor)
        {
            
            var query = Query();//_dbSet.AsQueryable();
            var cantidadRegistros = query.Count();
            var listaPaginada = ordenarPor(query)
                .Skip((pagina - 1) * cantidad)
                .Take(cantidad)
                .ToList();
            return (listaPaginada, cantidadRegistros);  
        }

        public virtual T? ObtenerPorId(int id)
        {
            return _dbSet.Find(id);
        }

        public List<T> ObtenerTodos()
        {
            return _dbSet
                .AsNoTracking()
                .ToList();
        }
        public IQueryable<T> Query()
        {
            return _dbSet.AsNoTracking();
                
        }
    }
}
