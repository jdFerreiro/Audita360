using FluentValidation;
using Audit360.Application.Features.Users.Commands;

namespace Audit360.Application.Validation.Users
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.User.Username).NotEmpty().MaximumLength(100);
            RuleFor(x => x.User.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.User.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.User.FullName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.User.RoleId).GreaterThan(0);
        }
    }
}