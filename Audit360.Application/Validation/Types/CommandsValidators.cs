using FluentValidation;
using Audit360.Application.Features.FindingTypes.Commands;
using Audit360.Application.Features.FindingSeverities.Commands;
using Audit360.Application.Features.FollowUpStatuses.Commands;

namespace Audit360.Application.Validation.Types
{
    public class CreateFindingTypeCommandValidator : AbstractValidator<CreateFindingTypeCommand>
    {
        public CreateFindingTypeCommandValidator()
        {
            RuleFor(x => x.FindingType.Description).NotEmpty();
        }
    }

    public class UpdateFindingTypeCommandValidator : AbstractValidator<UpdateFindingTypeCommand>
    {
        public UpdateFindingTypeCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.FindingType.Description).NotEmpty();
        }
    }

    public class DeleteFindingTypeCommandValidator : AbstractValidator<DeleteFindingTypeCommand>
    {
        public DeleteFindingTypeCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }

    public class CreateFindingSeverityCommandValidator : AbstractValidator<CreateFindingSeverityCommand>
    {
        public CreateFindingSeverityCommandValidator()
        {
            RuleFor(x => x.FindingSeverity.Description).NotEmpty();
        }
    }

    public class UpdateFindingSeverityCommandValidator : AbstractValidator<UpdateFindingSeverityCommand>
    {
        public UpdateFindingSeverityCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.FindingSeverity.Description).NotEmpty();
        }
    }

    public class DeleteFindingSeverityCommandValidator : AbstractValidator<DeleteFindingSeverityCommand>
    {
        public DeleteFindingSeverityCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }

    public class CreateFollowUpStatusCommandValidator : AbstractValidator<CreateFollowUpStatusCommand>
    {
        public CreateFollowUpStatusCommandValidator()
        {
            RuleFor(x => x.FollowUpStatus.Description).NotEmpty();
        }
    }

    public class UpdateFollowUpStatusCommandValidator : AbstractValidator<UpdateFollowUpStatusCommand>
    {
        public UpdateFollowUpStatusCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.FollowUpStatus.Description).NotEmpty();
        }
    }

    public class DeleteFollowUpStatusCommandValidator : AbstractValidator<DeleteFollowUpStatusCommand>
    {
        public DeleteFollowUpStatusCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}