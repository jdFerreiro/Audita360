using FluentValidation;
using Audit360.Application.Features.Responsibles.Commands;

namespace Audit360.Application.Validation.Responsibles
{
    public class CreateResponsibleCommandValidator : AbstractValidator<CreateResponsibleCommand>
    {
        public CreateResponsibleCommandValidator()
        {
            RuleFor(x => x.Responsible.Name).NotEmpty();
            RuleFor(x => x.Responsible.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Responsible.Area).NotEmpty();
        }
    }

    public class UpdateResponsibleCommandValidator : AbstractValidator<UpdateResponsibleCommand>
    {
        public UpdateResponsibleCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Responsible.Name).NotEmpty();
            RuleFor(x => x.Responsible.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Responsible.Area).NotEmpty();
        }
    }

    public class DeleteResponsibleCommandValidator : AbstractValidator<DeleteResponsibleCommand>
    {
        public DeleteResponsibleCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}