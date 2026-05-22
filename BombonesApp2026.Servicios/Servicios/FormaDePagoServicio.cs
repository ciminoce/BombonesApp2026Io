using BombonesApp2026.Datos;
using BombonesApp2026.Entidades;
using BombonesApp2026.Servicios.Common;
using BombonesApp2026.Servicios.DTOs.FormaDePago;
using BombonesApp2026.Servicios.Intefaces;
using BombonesApp2026.Servicios.Mapeadores;
using FluentValidation;

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
                _unitOfWork.FormasDePago.Editar(formaDePago,formaDePago.FormaDePagoId);
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
