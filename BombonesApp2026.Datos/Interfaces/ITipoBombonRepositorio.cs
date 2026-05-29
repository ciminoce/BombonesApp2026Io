using BombonesApp2026.Entidades;

namespace BombonesApp2026.Datos.Interfaces
{
    public interface ITipoBombonRepositorio:IRepositorioConcurrente<TipoBombon>
    {
        bool Existe(TipoBombon tipoBombon);
        bool EstaRelacionado(TipoBombon tipoBombon);
    }
}
