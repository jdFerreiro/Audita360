using FluentValidation;
using Audit360.Application.Features.FollowUps.Commands;

namespace Audit360.Application.Validation.FollowUps
{
    public class CreateFollowUpCommandValidator : AbstractValidator<CreateFollowUpCommand>
    {
        public CreateFollowUpCommandValidator()
        {
            RuleFor(x => x.FollowUp.Description).NotEmpty();
            RuleFor(x => x.FollowUp.StatusId).GreaterThan(0);
            RuleFor(x => x.FollowUp.FindingId).GreaterThan(0);
        }
    }

    public class UpdateFollowUpCommandValidator : AbstractValidator<UpdateFollowUpCommand>
    {
        public UpdateFollowUpCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.FollowUp.Description).NotEmpty();
            RuleFor(x => x.FollowUp.StatusId).GreaterThan(0);
            RuleFor(x => x.FollowUp.FindingId).GreaterThan(0);
        }
    }

    public class DeleteFollowUpCommandValidator : AbstractValidator<DeleteFollowUpCommand>
    {
        public DeleteFollowUpCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}