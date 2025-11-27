using FluentValidation;
using Audit360.Application.Features.Audits.Commands;

namespace Audit360.Application.Validation.Audits
{
    public class UpdateAuditCommandValidator : AbstractValidator<UpdateAuditCommand>
    {
        public UpdateAuditCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Audit.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Audit.Area).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Audit.StartDate).NotEmpty();
            RuleFor(x => x.Audit.StatusId).GreaterThan(0);
            RuleFor(x => x.Audit.ResponsibleId).GreaterThan(0);
        }
    }
}