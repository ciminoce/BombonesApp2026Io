using BombonesApp2026.Entidades;

namespace BombonesApp2026.Datos.Interfaces
{
    public interface ITipoBombonRepositorio:IRepositorioGenerico<TipoBombon>
    {
        bool Existe(TipoBombon tipoBombon);
        bool EstaRelacionado(TipoBombon tipoBombon);
    }
}
