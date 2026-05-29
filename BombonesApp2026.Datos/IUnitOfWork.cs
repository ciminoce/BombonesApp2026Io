using BombonesApp2026.Datos.Interfaces;

namespace BombonesApp2026.Datos
{
    public interface IUnitOfWork:IDisposable
    {
        ITipoBombonRepositorio TipoBombones { get; }
        IClienteRepositorio Clientes { get; }
        IFormaDePagoRepositorio FormasDePago{ get; }
        void Save();
        void RollBack();
    }
}
