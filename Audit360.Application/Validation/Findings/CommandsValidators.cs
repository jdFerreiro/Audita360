using FluentValidation;
using Audit360.Application.Features.Findings.Commands;

namespace Audit360.Application.Validation.Findings
{
    public class CreateFindingCommandValidator : AbstractValidator<CreateFindingCommand>
    {
        public CreateFindingCommandValidator()
        {
            RuleFor(x => x.Finding.Description).NotEmpty();
            RuleFor(x => x.Finding.TypeId).GreaterThan(0);
            RuleFor(x => x.Finding.SeverityId).GreaterThan(0);
            RuleFor(x => x.Finding.Date).NotEmpty();
            RuleFor(x => x.Finding.AuditId).GreaterThan(0);
        }
    }

    public class UpdateFindingCommandValidator : AbstractValidator<UpdateFindingCommand>
    {
        public UpdateFindingCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Finding.Description).NotEmpty();
            RuleFor(x => x.Finding.TypeId).GreaterThan(0);
            RuleFor(x => x.Finding.SeverityId).GreaterThan(0);
            RuleFor(x => x.Finding.Date).NotEmpty();
            RuleFor(x => x.Finding.AuditId).GreaterThan(0);
        }
    }

    public class DeleteFindingCommandValidator : AbstractValidator<DeleteFindingCommand>
    {
        public DeleteFindingCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}