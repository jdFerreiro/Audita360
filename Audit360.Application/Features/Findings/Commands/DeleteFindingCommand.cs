using MediatR;

namespace Audit360.Application.Features.Findings.Commands
{
    public record DeleteFindingCommand(int Id) : IRequest;
}