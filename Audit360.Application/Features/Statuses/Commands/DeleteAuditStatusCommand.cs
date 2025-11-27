using MediatR;

namespace Audit360.Application.Features.Statuses.Commands
{
    public record DeleteAuditStatusCommand(int Id) : IRequest;
}