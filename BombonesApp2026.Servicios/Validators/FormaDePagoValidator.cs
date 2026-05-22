using BombonesApp2026.Entidades;
using FluentValidation;

namespace BombonesApp2026.Servicios.Validators
{
    public class FormaDePagoValidator : AbstractValidator<FormaDePago>
    {
        public FormaDePagoValidator()
        {
            RuleFor(fp => fp.Nombre).NotEmpty().WithMessage("El nombre es requerido")
                .MaximumLength(50)
                .MinimumLength(3)
                .WithMessage("El nombre debe tener entre 3 y 50 caracteres");

        }
    }
}
