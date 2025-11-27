using MediatR;
using Audit360.Application.Features.Dto.Findings;

namespace Audit360.Application.Features.Findings.Commands
{
    public record CreateFindingCommand(FindingWriteDto Finding) : IRequest;
}