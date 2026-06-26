using BombonesApp2026.Datos;
using BombonesApp2026.Entidades;
using BombonesApp2026.Servicios.Common;
using BombonesApp2026.Servicios.DTOs.TipoBombon;
using BombonesApp2026.Servicios.Intefaces;
using BombonesApp2026.Servicios.Mapeadores;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BombonesApp2026.Servicios.Servicios
{
    //TODO: Agregar manejo de excepciones específicas para cada caso
    //y evitar el manejo genérico de excepciones, además de agregar manejo de concurrencia para evitar problemas en escenarios con múltiples usuarios editando o eliminando el mismo registro al mismo tiempo
    public class TipoBombonServicio : ITipoBombonServicio
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<TipoBombon> _validator;

        public TipoBombonServicio(IUnitOfWork unitOfWork,
            IValidator<TipoBombon> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public Result Agregar(TipoBombonCreateDto tipoBombonDto)
        {
            try
            {
                var tipoBombon=TipoBombonMapper.ToEntidad(tipoBombonDto);
                var result = _validator.Validate(tipoBombon);
                if (!result.IsValid)
                {
                    return Result.Failure(result.Errors.Select(e => e.ErrorMessage).ToList());
                }
                if (_unitOfWork.TipoBombones.Existe(tipoBombon))
                {
                    return Result.Failure($"Ya existe un tipo de bombón {tipoBombon.Nombre}");
                }
                _unitOfWork.TipoBombones.Agregar(tipoBombon);
                _unitOfWork.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                return Result.Failure($"Error al intentar agregar un tipo de bombón: {ex.Message}");
            }
        }

        public Result Borrar(TipoBombonDeleteDto tipoDto)
        {
            try
            {
                _unitOfWork.TipoBombones.Borrar(tipoDto.TipoBombonId,
                    tipoDto.RowVersion);
                _unitOfWork.Save();
                return Result.Success();
            }
            catch(DbUpdateConcurrencyException )
            {
                _unitOfWork.RollBack();
                return Result.ConcurrencyFailure("Otro usuario modificó el registro\nLa grilla se recargará automáticamente");

            }
            catch (KeyNotFoundException)
            {
                _unitOfWork.RollBack();
                return Result.Failure(@$"Tipo de bombon con ID: {tipoDto.TipoBombonId}
                        no econtrado");
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                return Result.Failure($"Error al intentar borrar un tipo de bombón {ex.Message}");
            }
        }

        public Result<List<TipoBombonListDto>> ObtenerTodos()
        {
            try
            {
                var lista = _unitOfWork.TipoBombones.ObtenerTodos();
                var listaDto = lista.Select(tb => TipoBombonMapper.ToListDto(tb)).ToList();
                return Result<List<TipoBombonListDto>>.Success(listaDto);
            }
            catch (Exception ex)
            {

                return Result<List<TipoBombonListDto>>.Failure(ex.Message);
            }
        }

        public Result<TipoBombonListDto> ObtenerPorId(int id)
        {
            try
            {
                var tipoBombon = _unitOfWork.TipoBombones.ObtenerPorId(id);
                if (tipoBombon != null)
                {
                    var tipoBombonDto = TipoBombonMapper.ToListDto(tipoBombon);
                    return Result<TipoBombonListDto>.Success(tipoBombonDto);

                }
                else
                {
                    return Result<TipoBombonListDto>.Failure("Tipo de bombón no encontrado!!!");
                }
            }
            catch (Exception ex)
            {

                return Result<TipoBombonListDto>.Failure(ex.Message);
            }
        }

        public Result<TipoBombonUpdateDto> ObtenerParaEditar(int id)
        {
            var tipoBombon = _unitOfWork.TipoBombones.ObtenerPorId(id);
            if(tipoBombon != null)
            {
                var tipoBombonDto = TipoBombonMapper.ToUpdateDto(tipoBombon);
                return Result<TipoBombonUpdateDto>.Success(tipoBombonDto);
            }
            else
            {
                return Result<TipoBombonUpdateDto>.Failure("Tipo de bombón no encontrado!!!");
            }
        }

        public Result Editar(TipoBombonUpdateDto dto)
        {
            try
            {
                var entidad = TipoBombonMapper.ToEntidad(dto);

                var validationResult = _validator.Validate(entidad);

                if (!validationResult.IsValid)
                {
                    return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage)
                            .ToList());
                }

                //if (_unitOfWork.TipoBombones.ObtenerPorId(dto.TipoBombonId) is null)
                //{
                //    return Result.Failure(
                //        "Tipo de bombón no encontrado");
                //}

                if (_unitOfWork.TipoBombones.Existe(entidad))
                {
                    return Result.Failure(
                        $" Ya existe un tipo de bombón con el nombre {entidad.Nombre}");
                }

                _unitOfWork.TipoBombones.Editar(entidad, entidad.TipoBombonId, dto.RowVersion);

                _unitOfWork.Save();

                return Result.Success();
            }
            catch (DbUpdateConcurrencyException)//acá decía DBConcurrencyException!!!
            {
                _unitOfWork.RollBack();

                return Result.ConcurrencyFailure(
                    "Otro usuario modificó el registro.\nLa grilla se recargará automáticamente");
            }
            catch (KeyNotFoundException)
            {
                _unitOfWork.RollBack();

                return Result.Failure(
                    $"Tipo de bombón con ID {dto.TipoBombonId} no encontrado");
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();

                return Result.Failure(
                    $"Error al intentar editar el tipo de bombón: {ex.Message}");
            }
        }
        public Result<List<TipoBombonListDto>> FiltrarPorActivo(bool activo)
        {
            try
            {
                var query = _unitOfWork.TipoBombones.Query();
                var lista=query.Where(tb=>tb.Activo == activo);
                var listaDto = lista.Select(tb => TipoBombonMapper.ToListDto(tb)).ToList();
                return Result<List<TipoBombonListDto>>.Success(listaDto);
            }
            catch (Exception ex)
            {

                return Result<List<TipoBombonListDto>>.Failure($"Error al intentar filtrar los tipos de Bombones: {ex.Message}");
            }
        }

        public Result<TipoBombonDeleteDto> ObtenerParaBorrar(int id)
        {
            var tipoBombon = _unitOfWork.TipoBombones.ObtenerPorId(id);
            if (tipoBombon != null)
            {
                var tipoBombonDto = TipoBombonMapper.ToDeleteDto(tipoBombon);
                return Result<TipoBombonDeleteDto>.Success(tipoBombonDto);
            }
            else
            {
                return Result<TipoBombonDeleteDto>.Failure($"Error al intentar obtener el tipo de bombón ");
            }
        }

        public Result<ResultadoPaginacionDto<TipoBombonListDto>> ObtenerPagina(int pagina, int cantidad)
        {
            try
            {
                var resultado = _unitOfWork.TipoBombones
                    .ObtenerPagina(pagina, cantidad);
                var listaDto = resultado.lista
                    .Select(tb => TipoBombonMapper.ToListDto(tb))
                    .ToList();
                var resultadoPaginado = new
                    ResultadoPaginacionDto<TipoBombonListDto>()
                {
                    Items = listaDto,
                    CantidadRegistros = resultado.totalRegistros,
                    CantidadPorPagina=cantidad,
                    PaginaActual= pagina
                };
                return Result<ResultadoPaginacionDto<TipoBombonListDto>>
                    .Success(resultadoPaginado);
            }
            catch (Exception ex)
            {

                return Result<ResultadoPaginacionDto<TipoBombonListDto>>
                    .Failure($"Error al intentar paginar: {ex.Message}");
            }
        }
    }
}
