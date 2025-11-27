using MediatR;
using Audit360.Application.Features.Dto.FindingSeverities;

namespace Audit360.Application.Features.FindingSeverities.Queries
{
    public record GetFindingSeverityByIdQuery(int Id) : IRequest<FindingSeverityReadDto?>;
}