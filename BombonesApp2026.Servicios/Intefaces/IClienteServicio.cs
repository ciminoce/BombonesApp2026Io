using BombonesApp2026.Servicios.Common;
using BombonesApp2026.Servicios.DTOs.Cliente;
using BombonesApp2026.Servicios.DTOs.FormaDePago;

namespace BombonesApp2026.Servicios.Intefaces
{
    public interface IClienteServicio
    {
        Result<List<ClienteListDto>> ObtenerTodos();
        Result<ResultadoPaginacionDto<ClienteListDto>> ObtenerPaginado(int paginaActual,
            int cantidadPorPagina, string campoOrdenar, bool esAscendente, bool? filtroActivo);

        Result<ClienteListDto> ObtenerPorId(int id);
        Result<ClienteUpdateDto> ObtenerParaEditar(int id);
        Result<int> Agregar(ClienteCreateDto clienteDto);
        Result Editar(ClienteUpdateDto clienteDto);
        Result Borrar(int id);

    }
}
