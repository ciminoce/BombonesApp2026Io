using BombonesApp2026.Entidades;
using BombonesApp2026.Servicios.DTOs.Cliente;

namespace BombonesApp2026.Servicios.Mapeadores
{
    public static class ClienteMapper
    {
        public static ClienteListDto ToListDto(Cliente cliente)
        {
            return new ClienteListDto
            {
                ClienteId = cliente.ClienteId,
                NombreCompleto = $"{cliente.Nombre} {cliente.Apellido}",
                Documento = cliente.Documento,
                Telefono = cliente.Telefono,
                Email = cliente.Email,
                Activo = cliente.Activo
            };
        }
        public static Cliente ToEntidad(ClienteCreateDto clienteDto)
        {
            return new Cliente
            {
                Nombre = clienteDto.Nombre,
                Apellido = clienteDto.Apellido,
                Documento = clienteDto.Documento,
                Telefono = clienteDto.Telefono,
                Email = clienteDto.Email,
                Calle = clienteDto.Calle,
                Numero = clienteDto.Numero,
                Localidad = clienteDto.Localidad,
                Provincia = clienteDto.Provincia,
                CodigoPostal = clienteDto.CodigoPostal
            };
        }
        public static Cliente ToEntidad(ClienteUpdateDto clienteDto)
        {
            return new Cliente
            {
                ClienteId = clienteDto.ClienteId,
                Nombre = clienteDto.Nombre,
                Apellido = clienteDto.Apellido,
                Documento = clienteDto.Documento,
                Telefono = clienteDto.Telefono,
                Email = clienteDto.Email,
                Calle = clienteDto.Calle,
                Numero = clienteDto.Numero,
                Localidad = clienteDto.Localidad,
                Provincia = clienteDto.Provincia,
                CodigoPostal = clienteDto.CodigoPostal,
                Activo = clienteDto.Activo,
                RowVersion = clienteDto.RowVersion
            };
        }
    }
}
