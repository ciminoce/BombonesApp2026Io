using BombonesApp2026.Datos;
using BombonesApp2026.Entidades;
using BombonesApp2026.Servicios.Common;
using BombonesApp2026.Servicios.DTOs.Caja;
using CajaesApp2026.Servicios.Intefaces;
using CajaesApp2026.Servicios.Mapeadores;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CajasApp2026.Servicios.Servicios
{
    public class CajaServicio : ICajaServicio
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CajaCreateDto> _createValidator;
        private readonly IValidator<CajaUpdateDto> _updateValidator;

        public CajaServicio(
            IUnitOfWork unitOfWork,
            IValidator<CajaCreateDto> createValidator,
            IValidator<CajaUpdateDto> updateValidator)
        {
            _unitOfWork = unitOfWork;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public Result<int> Agregar(CajaCreateDto CajaDto)
        {
            // 1. Validar el DTO directamente
            var validationResult = _createValidator.Validate(CajaDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<int>.Failure(errors);
            }

            // 2. Mapear a entidad
            var Caja = CajaMapper.ToEntidad(CajaDto);

            // 3. Regla de negocio
            if (_unitOfWork.Cajas.Existe(Caja))
            {
                return Result<int>.Failure("El Caja ya existe.");
            }

            try
            {
                _unitOfWork.Cajas.Agregar(Caja);
                _unitOfWork.Save();
                return Result<int>.Success(Caja.ProductoId);
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                return Result<int>.Failure($"Error al intentar agregar el Caja: {ex.Message}");
            }
        }

        public Result Editar(CajaUpdateDto CajaDto)
        {
            // 1. Validar el DTO de actualización
            var validationResult = _updateValidator.Validate(CajaDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result.Failure(errors);
            }

            // 2. Obtener la entidad persistida
            var caja = _unitOfWork.Cajas.ObtenerPorId(CajaDto.ProductoId);
            if (caja == null)
            {
                return Result.Failure("Caja no encontrado.");
            }

            // 3. Actualizar valores
            caja.Nombre = CajaDto.Nombre;
            caja.Descripcion = CajaDto.Descripcion;
            caja.Precio = CajaDto.Precio;
            caja.EsSurtida = CajaDto.EsSurtida;
            caja.CantidadBombones = CajaDto.CantidadBombones;
            caja.Stock = CajaDto.Stock;

            caja.Activo = CajaDto.Activo;
            caja.RowVersion = CajaDto.RowVersion;

            // 4. Regla de negocio para duplicados en edición
            if (_unitOfWork.Cajas.Existe(caja))
            {
                return Result.Failure("Ya existe otro Caja registrado con los mismos datos.");
            }

            try
            {
                _unitOfWork.Cajas.Editar(caja, caja.ProductoId);
                _unitOfWork.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                return Result.Failure($"Error al intentar editar el Caja: {ex.Message}");
            }
        }

        public Result Borrar(int id)
        {
            var caja = _unitOfWork.Cajas.ObtenerPorId(id);
            if (caja == null)
            {
                return Result.Failure("Caja no encontrado.");
            }

            try
            {
                _unitOfWork.Cajas.Borrar(caja.ProductoId);
                _unitOfWork.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                return Result.Failure($"Error al intentar borrar el Caja: {ex.Message}");
            }
        }

        public Result<List<CajaListDto>> ObtenerTodos()
        {
            try
            {
                var lista = _unitOfWork.Cajas.ObtenerTodos();
                var listaDto = lista.Select(b => b.ToListDto()).ToList();

                return Result<List<CajaListDto>>.Success(listaDto);
            }
            catch (Exception ex)
            {
                return Result<List<CajaListDto>>.Failure(ex.Message);
            }
        }

        public Result<CajaListDto> ObtenerPorId(int id)
        {
            try
            {
                var caja = _unitOfWork.Cajas.ObtenerPorId(id);
                if (caja == null)
                {
                    return Result<CajaListDto>.Failure("Caja no encontrado.");
                }

                var CajaDto = caja.ToListDto();

                return Result<CajaListDto>.Success(CajaDto);
            }
            catch (Exception ex)
            {
                return Result<CajaListDto>.Failure(ex.Message);
            }
        }

        public Result<CajaUpdateDto> ObtenerParaEditar(int id)
        {
            try
            {
                var caja = _unitOfWork.Cajas.ObtenerPorId(id);
                if (caja == null)
                {
                    return Result<CajaUpdateDto>.Failure("Caja no encontrado.");
                }

                var CajaDto = caja.ToUpdateDto();
                return Result<CajaUpdateDto>.Success(CajaDto);
            }
            catch (Exception ex)
            {
                return Result<CajaUpdateDto>.Failure(ex.Message);
            }
        }
        public Result<ResultadoPaginacionDto<CajaListDto>> ObtenerPaginado(
                int pagina,
                int cantidad,
                string campoOrden,
                bool esAscendente,
                bool? filtroActivo = null,
                string? textoBuscar = null)
        {
            try
            {
                Expression<Func<Caja, bool>>? filtro = b =>
                        (!filtroActivo.HasValue || b.Activo == filtroActivo.Value) &&
                        (string.IsNullOrWhiteSpace(textoBuscar) || b.Nombre.Contains(textoBuscar));
                Func<IQueryable<Caja>, IOrderedQueryable<Caja>>? ordenarPor = campoOrden switch
                {
                    "TipoCajaId" => q => esAscendente ? q.OrderBy(c => c.ProductoId) : q.OrderByDescending(c => c.ProductoId),
                    "Nombre" => q => esAscendente ? q.OrderBy(c => c.Nombre) : q.OrderByDescending(c => c.Nombre),
                    _ => q => esAscendente ? q.OrderBy(c => c.Nombre) : q.OrderByDescending(c => c.Nombre),
                };
                var resultado = _unitOfWork.Cajas.ObtenerPagina(pagina, cantidad, ordenarPor, filtro);
                var listaDto = resultado.lista
                    .Select(b => b.ToListDto()).ToList();

                var resultadoPaginado = new ResultadoPaginacionDto<CajaListDto>()
                {
                    Items = listaDto,
                    CantidadRegistros = resultado.totalRegistros,
                    CantidadPorPagina = cantidad,
                    PaginaActual = pagina
                };

                return Result<ResultadoPaginacionDto<CajaListDto>>.Success(resultadoPaginado);
            }
            catch (Exception ex)
            {
                return Result<ResultadoPaginacionDto<CajaListDto>>.Failure($"Error al intentar paginar: {ex.Message}");
            }
        }
        public Result<CajaDeleteDto> ObtenerParaBorrar(int id)
        {
            var caja = _unitOfWork.Cajas.ObtenerPorId(id);
            if (caja != null)
            {
                var CajaDto = CajaMapper.ToDeleteDto(caja);
                return Result<CajaDeleteDto>.Success(CajaDto);
            }

            return Result<CajaDeleteDto>.Failure("Error al intentar obtener el bombón");
        }
        public Result Borrar(CajaDeleteDto CajaDto)
        {
            try
            {
                _unitOfWork.Cajas.Borrar(CajaDto.ProductoId, CajaDto.RowVersion);
                _unitOfWork.Save();
                return Result.Success();
            }
            catch (DbUpdateConcurrencyException)
            {
                _unitOfWork.RollBack();
                return Result.ConcurrencyFailure("Otro usuario modificó el registro\nLa grilla se recargará automáticamente");
            }
            catch (KeyNotFoundException)
            {
                _unitOfWork.RollBack();
                return Result.Failure($"Bombón con ID: {CajaDto.ProductoId} no encontrado");
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                return Result.Failure($"Error al intentar borrar un bombón: {ex.Message}");
            }
        }


        public Result<int> ObtenerPaginaRegistro(int seleccionadoId, int cantidadPorPagina, bool? filtroActivo = null)
        {
            try
            {
                Expression<Func<Caja, bool>>? filtrarPor = null;
                if (filtroActivo is not null)
                {
                    filtrarPor = b => b.Activo == filtroActivo;
                }

                var posicion = _unitOfWork.Cajas.ObtenerPosicionRegistro(seleccionadoId, filtrarPor);
                var pagina = (int)Math.Ceiling((double)posicion / cantidadPorPagina);
                return Result<int>.Success(pagina);
            }
            catch (Exception ex)
            {
                return Result<int>.Failure($"Error al intentar obtener la página: {ex.Message}");
            }
        }

    }
}
