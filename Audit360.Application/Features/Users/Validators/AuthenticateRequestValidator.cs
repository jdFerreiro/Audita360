using FluentValidation;
using Audit360.Application.Features.Dto.Users;

namespace Audit360.Application.Features.Users.Validators
{
    public class AuthenticateRequestValidator : AbstractValidator<AuthenticateRequestDto>
    {
        public AuthenticateRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo es obligatorio.")
                .EmailAddress().WithMessage("El correo no tiene un formato válido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");
        }
    }
}
