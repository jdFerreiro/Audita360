using MediatR;
using Audit360.Application.Features.Dto.FindingSeverities;

namespace Audit360.Application.Features.FindingSeverities.Commands
{
    public record CreateFindingSeverityCommand(FindingSeverityWriteDto FindingSeverity) : IRequest;
}