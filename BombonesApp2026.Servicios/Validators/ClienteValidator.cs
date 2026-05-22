using BombonesApp2026.Entidades;
using FluentValidation;

namespace BombonesApp2026.Servicios.Validators
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(c => c.Nombre).NotEmpty().WithMessage("El nombre es requerido")
                .MaximumLength(50)
                .MinimumLength(3)
                .WithMessage("El nombre debe tener entre 3 y 50 caracteres");

            RuleFor(c=>c.Apellido).NotEmpty().WithMessage("El apellido es requerido")
                .MaximumLength(50)
                .MinimumLength(3)
                .WithMessage("El apellido debe tener entre 3 y 50 caracteres");
            RuleFor(c=>c.Documento).NotEmpty().WithMessage("El documento es requerido")
                .MaximumLength(20)
                .MinimumLength(5)
                .WithMessage("El documento debe tener entre 5 y 20 caracteres");
            RuleFor(c => c.Email).MaximumLength(100).WithMessage("El email no puede exceder los 100 caracteres")
                .EmailAddress().WithMessage("El email no es válido");
            RuleFor(c => c.Telefono).MaximumLength(30).WithMessage("El teléfono no puede exceder los 30 caracteres")
                .MinimumLength(7).WithMessage("El teléfono debe tener al menos 7 caracteres");
            RuleFor(c => c.Calle).MaximumLength(100).WithMessage("La calle no puede exceder los 100 caracteres");
            RuleFor(c => c.Numero).MaximumLength(20).WithMessage("El número no puede exceder los 20 caracteres");
            RuleFor(c=>c.Localidad).MaximumLength(100).WithMessage("La localidad no puede exceder los 100 caracteres");
            RuleFor(c => c.Provincia).MaximumLength(100).WithMessage("La provincia no puede exceder los 100 caracteres");
            RuleFor(c => c.CodigoPostal).MaximumLength(10).WithMessage("El código postal no puede exceder los 10 caracteres");
        }
    }
}
