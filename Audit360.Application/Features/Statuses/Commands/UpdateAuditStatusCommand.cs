using MediatR;
using Audit360.Application.Features.Dto.Statuses;

namespace Audit360.Application.Features.Statuses.Commands
{
    public record UpdateAuditStatusCommand(int Id, AuditStatusWriteDto AuditStatus) : IRequest;
}