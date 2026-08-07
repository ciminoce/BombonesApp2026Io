using BombonesApp2026.Datos;
using BombonesApp2026.Entidades;
using BombonesApp2026.Servicios.Common;
using BombonesApp2026.Servicios.DTOs.FormaDePago;
using BombonesApp2026.Servicios.Intefaces;
using BombonesApp2026.Servicios.Mapeadores;
using FluentValidation;
using System.Linq.Expressions;

namespace BombonesApp2026.Servicios.Servicios
{
    public class FormaDePagoServicio : IFormaDePagoServicio
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<FormaDePago> _validator;

        public FormaDePagoServicio(IUnitOfWork unitOfWork,
            IValidator<FormaDePago> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public Result Agregar(FormaDePagoCreateDto formaDePagoDto)
        {
            var formaDePago = FormaDePagoMapper.ToEntidad(formaDePagoDto);
            var result = _validator.Validate(formaDePago);
            if (!result.IsValid)
            {
                return Result.Failure(result.Errors.Select(e => e.ErrorMessage).ToList());
            }
            if (_unitOfWork.FormasDePago.Existe(formaDePago))
            {
                return Result.Failure("Forma de pago already exist!!!");
            }
            try
            {
                _unitOfWork.FormasDePago.Agregar(formaDePago);
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
            var formaDePago = _unitOfWork.FormasDePago.ObtenerPorId(id);
            if (formaDePago == null)
            {
                return Result.Failure("Forma de pago no encontrada!!!");
            }
            //Luego ver si no está relacionada con otras entidades, como ventas, para no eliminarla físicamente
            try
            {
                _unitOfWork.FormasDePago.Borrar(formaDePago.FormaDePagoId);
                _unitOfWork.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {

                return Result.Failure(ex.Message);
            }
        }

        public Result<List<FormaDePagoListDto>> ObtenerTodos()
        {
            try
            {
                var lista = _unitOfWork.FormasDePago.ObtenerTodos();
                var listaDto = lista.Select(FormaDePagoMapper.ToListDto).ToList();
                return Result<List<FormaDePagoListDto>>.Success(listaDto);

            }
            catch (Exception ex)
            {

                return Result<List<FormaDePagoListDto>>.Failure(ex.Message);
            }
        }
        public Result<ResultadoPaginacionDto<FormaDePagoListDto>> ObtenerPaginado(int pagina,
            int registros, string campoOrden, bool esAscendente,
            bool? filtroActivo = null)
        {
            try
            {
                Expression<Func<FormaDePago, bool>>? filtradoPor = null;
                if (filtroActivo is not null)
                {
                    filtradoPor = fp => fp.Activo == filtroActivo;
                }
                Func<IQueryable<FormaDePago>,IOrderedQueryable<FormaDePago>>? ordenarPor = null;
                switch(campoOrden)
                {
                    case "FormaDePagoId":
                        ordenarPor = esAscendente ? (q => q.OrderBy(fp => fp.FormaDePagoId)) : (q => q.OrderByDescending(fp => fp.FormaDePagoId));
                        break;
                    case "Nombre":
                    default:
                        ordenarPor = esAscendente ? (q => q.OrderBy(fp => fp.Nombre)) : (q => q.OrderByDescending(fp => fp.Nombre));
                        break;
                }
                var resultado=_unitOfWork.FormasDePago.ObtenerPagina(pagina, registros, ordenarPor, filtradoPor);
                
                var listaDto = resultado.lista.Select(FormaDePagoMapper.ToListDto).ToList();
                var resultadoPaginacion = new ResultadoPaginacionDto<FormaDePagoListDto>
                {
                    Items = listaDto,
                    CantidadRegistros = resultado.totalRegistros,
                    CantidadPorPagina = registros,
                    PaginaActual = pagina
                };
                return Result<ResultadoPaginacionDto<FormaDePagoListDto>>.Success(resultadoPaginacion);
            }
            catch (Exception ex)
            {
                return Result<ResultadoPaginacionDto<FormaDePagoListDto>>.Failure(ex.Message);
            }
        }

        public Result<FormaDePagoListDto> ObtenerPorId(int id)
        {
            try
            {
                var formaDePago = _unitOfWork.FormasDePago.ObtenerPorId(id);
                if (formaDePago == null)
                {
                    return Result<FormaDePagoListDto>.Failure("Forma de pago no encontrada!!!");
                }
                var formaDePagoDto = FormaDePagoMapper.ToListDto(formaDePago);
                return Result<FormaDePagoListDto>.Success(formaDePagoDto);

            }
            catch (Exception ex)
            {

                return Result<FormaDePagoListDto>.Failure(ex.Message);
            }
        }

        public Result<FormaDePagoUpdateDto> ObtenerParaEditar(int id)
        {
            try
            {
                var formaDePago = _unitOfWork.FormasDePago.ObtenerPorId(id);
                if (formaDePago != null)
                {
                    var formaDePagoDto = FormaDePagoMapper.ToUpdateDto(formaDePago);
                    return Result<FormaDePagoUpdateDto>.Success(formaDePagoDto);
                }
                return Result<FormaDePagoUpdateDto>.Failure("Forma de pago no encontrada!!!");
            }
            catch (Exception ex)
            {

                return Result<FormaDePagoUpdateDto>.Failure(ex.Message);
            }
        }

        public Result Editar(FormaDePagoUpdateDto formaDePagoDto)
        {
            var formaDePagoToValidate = FormaDePagoMapper.ToEntidad(formaDePagoDto);
            var result = _validator.Validate(formaDePagoToValidate);
            if (!result.IsValid)
            {
                return Result.Failure(result.Errors.Select(e => e.ErrorMessage).ToList());
            }
            var formaDePago = _unitOfWork.FormasDePago.ObtenerPorId(formaDePagoDto.FormaDePagoId);
            if (formaDePago == null)
            {
                return Result.Failure("Forma de pago no encontrada!!!");
            }
            formaDePago.Nombre = formaDePagoDto.Nombre;
            formaDePago.Activo = formaDePagoDto.Activo;

            if (_unitOfWork.FormasDePago.Existe(formaDePago))
            {
                return Result.Failure("Forma de pago existentet!!!");
            }
            try
            {
                _unitOfWork.FormasDePago.Editar(formaDePago, formaDePago.FormaDePagoId);
                _unitOfWork.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {

                return Result.Failure(ex.Message);
            }
        }

        public Result<List<FormaDePagoListDto>> FiltrarPorActivo(bool activo)
        {
            try
            {
                var query = _unitOfWork.FormasDePago.Query();
                var lista = query.Where(tb => tb.Activo == activo);
                var listaDto = lista.Select(tb => FormaDePagoMapper.ToListDto(tb)).ToList();
                return Result<List<FormaDePagoListDto>>.Success(listaDto);
            }
            catch (Exception ex)
            {

                return Result<List<FormaDePagoListDto>>.Failure($"Error al intentar filtrar las formas de pago: {ex.Message}");
            }
        }
    }
}
