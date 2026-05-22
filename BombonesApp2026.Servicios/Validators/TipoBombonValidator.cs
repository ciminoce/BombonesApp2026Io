using BombonesApp2026.Entidades;
using FluentValidation;

namespace BombonesApp2026.Servicios.Validators
{
    public class TipoBombonValidator : AbstractValidator<TipoBombon>
    {
        public TipoBombonValidator()
        {
            RuleFor(tb => tb.Nombre).NotEmpty().WithMessage("El nombre es requerido")
                .MaximumLength(50)
                .MinimumLength(3)
                .WithMessage("El nombre debe tener entre 3 y 50 caracteres");
            RuleFor(tb => tb.Descripcion).MaximumLength(200)
                .WithMessage("La descripción no puede exceder los 200 caracteres");
        }
    }
}
