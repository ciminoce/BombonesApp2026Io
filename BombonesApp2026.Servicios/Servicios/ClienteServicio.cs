using BombonesApp2026.Datos;
using BombonesApp2026.Entidades;
using BombonesApp2026.Servicios.Common;
using BombonesApp2026.Servicios.DTOs.Cliente;
using BombonesApp2026.Servicios.Intefaces;
using BombonesApp2026.Servicios.Mapeadores;
using FluentValidation;

namespace BombonesApp2026.Servicios.Servicios
{
    public class ClienteServicio : IClienteServicio
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Cliente> _validator;
        public ClienteServicio(IUnitOfWork unitOfWork, IValidator<Cliente> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public Result Agregar(ClienteCreateDto clienteDto)
        {
            var  cliente=ClienteMapper.ToEntidad(clienteDto);
            var validationResult = _validator.Validate(cliente);
            if(!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result.Failure(errors);
            }
            if(_unitOfWork.Clientes.Existe(cliente))
            {
                return Result.Failure("El cliente ya existe");
            }
            try
            {
                _unitOfWork.Clientes.Agregar(cliente);
                _unitOfWork.Save();
                return Result.Success();

            }
            catch (Exception ex)
            {

                return Result.Failure(ex.Message);
            } 
        }

        public Result Borrar(int id)
        {
            var cliente = _unitOfWork.Clientes.ObtenerPorId(id);
            if (cliente == null)
            {
                return Result.Failure("Cliente no encontrado");
            }
            //verificar si el cliente tiene ventas asociadas
            try
            {
                _unitOfWork.Clientes.Borrar(cliente.ClienteId);
                _unitOfWork.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {

                return Result.Failure(ex.Message);
            }
        }

        public Result<List<ClienteListDto>> ObtenerTodos()
        {
            try
            {
                var lista = _unitOfWork.Clientes.ObtenerTodos();
                var listaDto = lista.Select(c => new ClienteListDto
                {
                    ClienteId = c.ClienteId,
                    NombreCompleto = $"{c.Nombre} {c.Apellido}",
                    Documento = c.Documento,
                    Telefono = c.Telefono,
                    Email = c.Email,
                    Activo = c.Activo
                }).ToList();
                return Result<List<ClienteListDto>>.Success(listaDto);
            }
            catch (Exception ex)
            {

                return Result<List<ClienteListDto>>.Failure(ex.Message);
            }
        }

        public Result<ClienteListDto> ObtenerPorId(int id)
        {
            try
            {
                var cliente = _unitOfWork.Clientes.ObtenerPorId(id);
                if( cliente == null)
                {
                    return Result<ClienteListDto>.Failure("Cliente no encontrado");
                }
                var clienteDto = new ClienteListDto
                {
                    ClienteId = cliente.ClienteId,
                    NombreCompleto = $"{cliente.Nombre} {cliente.Apellido}",
                    Documento = cliente.Documento,
                    Telefono = cliente.Telefono,
                    Email = cliente.Email,
                    Activo = cliente.Activo
                };
                return Result<ClienteListDto>.Success(clienteDto);
            }
            catch (Exception ex)
            {

                return Result<ClienteListDto>.Failure(ex.Message);
            }
        }

        public Result<ClienteUpdateDto> ObtenerParaEditar(int id)
        {
            try
            {
                var cliente = _unitOfWork.Clientes.ObtenerPorId(id);
                if ( cliente == null)
                {
                    return Result<ClienteUpdateDto>.Failure("Cliente no encontrado");
                }
                var clienteDto = new ClienteUpdateDto
                {
                    ClienteId = cliente.ClienteId,
                    Nombre = cliente.Nombre,
                    Apellido = cliente.Apellido,
                    Documento = cliente.Documento,
                    Telefono = cliente.Telefono,
                    Email = cliente.Email,
                    Calle = cliente.Calle,
                    Numero = cliente.Numero,
                    Localidad = cliente.Localidad,
                    Provincia = cliente.Provincia,
                    CodigoPostal = cliente.CodigoPostal,
                    Activo = cliente.Activo,
                    RowVersion = cliente.RowVersion
                };
                return Result<ClienteUpdateDto>.Success(clienteDto);
            }
            catch (Exception ex)
            {

                return Result<ClienteUpdateDto>.Failure(ex.Message);
            }
        }

        public Result Editar(ClienteUpdateDto clienteDto)
        {
            var clienteToValidate = ClienteMapper.ToEntidad(clienteDto);
            var validationResult = _validator.Validate(clienteToValidate);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result.Failure(errors);
            }
            var cliente = _unitOfWork.Clientes.ObtenerPorId(clienteDto.ClienteId);
            if (cliente == null)
            {
                return Result.Failure("Cliente no encontrado");
            }
            cliente.Nombre = clienteDto.Nombre;
            cliente.Apellido = clienteDto.Apellido;
            cliente.Documento = clienteDto.Documento;
            cliente.Telefono = clienteDto.Telefono;
            cliente.Email = clienteDto.Email;
            cliente.Calle = clienteDto.Calle;
            cliente.Numero = clienteDto.Numero;
            cliente.Localidad = clienteDto.Localidad;
            cliente.Provincia = clienteDto.Provincia;
            cliente.CodigoPostal = clienteDto.CodigoPostal;
            cliente.Activo = clienteDto.Activo;
            try
            {
                _unitOfWork.Clientes.Editar(cliente, cliente.ClienteId);
                _unitOfWork.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
