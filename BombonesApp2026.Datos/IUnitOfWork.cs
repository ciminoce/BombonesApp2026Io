using BombonesApp2026.Datos.Interfaces;

namespace BombonesApp2026.Datos
{
    public interface IUnitOfWork
    {
        ITipoBombonRepositorio TipoBombones { get; }
        IClienteRepositorio Clientes { get; }
        IFormaDePagoRepositorio FormasDePago{ get; }
        void Save();
    }
}
