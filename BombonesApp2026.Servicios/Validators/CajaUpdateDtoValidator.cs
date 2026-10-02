using BombonesApp2026.Servicios.DTOs.Caja;
using FluentValidation;

namespace BombonesApp2026.Servicios.Validators
{
    public class CajaUpdateDtoValidator : AbstractValidator<CajaUpdateDto>
    {
        public CajaUpdateDtoValidator()
        {
            RuleFor(x => x.Nombre)
    .NotEmpty().WithMessage("El nombre es obligatorio.")
    .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.");

            RuleFor(x => x.Descripcion)
                .MaximumLength(250).WithMessage("La descripción no puede superar los 200 caracteres.");

            RuleFor(x => x.Precio)
                .NotEmpty().WithMessage("El precio es obligatorio.")
                .GreaterThan(0).WithMessage("El precio debe ser un valor positivo.");
            RuleFor(x => x.CantidadBombones)
                .NotEmpty().WithMessage("La cantidad es obligatoria.")
                .GreaterThan(0).WithMessage("El cantidad debe ser un valor positivo.");

            RuleFor(x => x.Stock)
                .NotEmpty().WithMessage("El stock es obligatorio.")
                .GreaterThanOrEqualTo(0).WithMessage("El stock debe ser un valor no negativo.");
            RuleFor(x => x.RowVersion)
                .NotNull().WithMessage("El sello de concurrencia es obligatorio.");

        }
    }
}
