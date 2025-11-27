using MediatR;
using Audit360.Application.Features.Dto.Findings;

namespace Audit360.Application.Features.Findings.Queries
{
    public record GetFindingByIdQuery(int Id) : IRequest<FindingReadDto?>;
}