using BombonesApp2026.Datos.Interfaces;
using BombonesApp2026.Entidades;

namespace BombonesApp2026.Datos.Repositorios
{
    public class TipoBombonRepositorio : RepositorioGenerico<TipoBombon>, ITipoBombonRepositorio
    {
        public TipoBombonRepositorio(BombonesDbContext context) : base(context)
        {
        }

        public bool EstaRelacionado(TipoBombon tipoBombon)
        {
            return false;
        }

        public bool Existe(TipoBombon tipoBombon)
        {
            return _context.TipoBombones
                .Any(tb => tb.Nombre == tipoBombon.Nombre &&
                tb.TipoBombonId != tipoBombon.TipoBombonId);
        }

    }
}
