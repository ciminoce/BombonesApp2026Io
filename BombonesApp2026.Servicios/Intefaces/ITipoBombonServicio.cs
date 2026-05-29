using BombonesApp2026.Servicios.Common;
using BombonesApp2026.Servicios.DTOs.TipoBombon;

namespace BombonesApp2026.Servicios.Intefaces
{
    public interface ITipoBombonServicio
    {
        Result<List<TipoBombonListDto>> ObtenerTodos();
        Result<TipoBombonListDto> ObtenerPorId(int id);
        Result<TipoBombonUpdateDto> ObtenerParaEditar(int id);
        Result<TipoBombonDeleteDto> ObtenerParaBorrar(int id);

        Result Agregar(TipoBombonCreateDto tipoBombonDto);
        Result Editar(TipoBombonUpdateDto tipoBombonDto);
        Result Borrar(TipoBombonDeleteDto tipoBombonDto);
        Result<List<TipoBombonListDto>> FiltrarPorActivo(bool activo);

    }
}
