using BombonesApp2026.Datos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BombonesApp2026.Datos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BombonesDbContext _context;
        public ITipoBombonRepositorio TipoBombones { get; }

        public IClienteRepositorio Clientes { get; }

        public IFormaDePagoRepositorio FormasDePago { get; }

        public UnitOfWork(IFormaDePagoRepositorio formaDePagoRepositorio,
            IClienteRepositorio clienteRepositorio, 
            ITipoBombonRepositorio tipoBombonRepositorio,
            BombonesDbContext context)
        {
            FormasDePago = formaDePagoRepositorio;
            Clientes = clienteRepositorio;
            TipoBombones = tipoBombonRepositorio;
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void RollBack()
        {
            foreach (var item in _context.ChangeTracker.Entries())
            {
                switch (item.State)
                {
                    case EntityState.Modified:
                        item.State = EntityState.Unchanged;
                        item.CurrentValues.SetValues(item.OriginalValues);
                        break;
                    case EntityState.Added:
                        item.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        item.State = EntityState.Unchanged;
                        break;
                }
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
