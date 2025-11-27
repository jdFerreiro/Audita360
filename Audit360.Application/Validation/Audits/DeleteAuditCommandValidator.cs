using FluentValidation;
using Audit360.Application.Features.Audits.Commands;

namespace Audit360.Application.Validation.Audits
{
    public class DeleteAuditCommandValidator : AbstractValidator<DeleteAuditCommand>
    {
        public DeleteAuditCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}