using FluentValidation;
using Audit360.Application.Features.Users.Commands;

namespace Audit360.Application.Validation.Users
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}