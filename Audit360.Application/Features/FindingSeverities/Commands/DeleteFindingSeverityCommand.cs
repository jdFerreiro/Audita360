using MediatR;

namespace Audit360.Application.Features.FindingSeverities.Commands
{
    public record DeleteFindingSeverityCommand(int Id) : IRequest<MediatR.Unit>;
}