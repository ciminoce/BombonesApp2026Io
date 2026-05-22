using BombonesApp2026.Datos.Interfaces;

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
    }
}
