using BombonesApp2026.Servicios.Common;
using BombonesApp2026.Servicios.DTOs.Cliente;

namespace BombonesApp2026.Servicios.Intefaces
{
    public interface IClienteServicio
    {
        Result<List<ClienteListDto>> ObtenerTodos();
        Result<ClienteListDto> ObtenerPorId(int id);
        Result<ClienteUpdateDto> ObtenerParaEditar(int id);
        Result Agregar(ClienteCreateDto clienteDto);
        Result Editar(ClienteUpdateDto clienteDto);
        Result Borrar(int id);

    }
}
