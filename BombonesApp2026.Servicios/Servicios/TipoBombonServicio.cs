using BombonesApp2026.Datos;
using BombonesApp2026.Entidades;
using BombonesApp2026.Servicios.Common;
using BombonesApp2026.Servicios.DTOs.TipoBombon;
using BombonesApp2026.Servicios.Intefaces;
using BombonesApp2026.Servicios.Mapeadores;
using FluentValidation;

namespace BombonesApp2026.Servicios.Servicios
{
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
            var tipoBombon=TipoBombonMapper.ToEntidad(tipoBombonDto);
            var result = _validator.Validate(tipoBombon);
            if (!result.IsValid)
            {
                return Result.Failure(result.Errors.Select(e => e.ErrorMessage).ToList());
            }
            if (_unitOfWork.TipoBombones.Existe(tipoBombon))
            {
                return Result.Failure("Tipo de bombón existente!!!");
            }
            try
            {
                _unitOfWork.TipoBombones.Agregar(tipoBombon);
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
            var tipoBombon = _unitOfWork.TipoBombones.ObtenerPorId(id);
            if (tipoBombon == null)
            {
                return Result.Failure("Tipo de bombón no encontrado!!!");
            }
            try
            {
                _unitOfWork.TipoBombones.Borrar(tipoBombon.TipoBombonId);
                _unitOfWork.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {

                return Result.Failure(ex.Message);
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

        public Result Editar(TipoBombonUpdateDto tipoBombonDto)
        {
            var tipoBombonToValidate = TipoBombonMapper.ToEntidad(tipoBombonDto);
            var result = _validator.Validate(tipoBombonToValidate);
            if(!result.IsValid)
            {
                return Result.Failure(result.Errors.Select(e => e.ErrorMessage).ToList());
            }
            var tipoBombon = _unitOfWork.TipoBombones.ObtenerPorId(tipoBombonDto.TipoBombonId);
            if (tipoBombon == null)
            {
                return Result.Failure("Tipo de bombón no encontrado!!!");
            }
            // Actualizar las propiedades del tipo de bombón
            tipoBombon.Nombre = tipoBombonDto.Nombre;
            tipoBombon.Activo = tipoBombonDto.Activo;
            tipoBombon.RowVersion = tipoBombonDto.RowVersion;
            if(_unitOfWork.TipoBombones.Existe(tipoBombon))
            {
                return Result.Failure("Tipo de bombón existente!!!");
            }
            try
            {
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
