using MediatR;
using Audit360.Application.Features.Dto.Responsibles;

namespace Audit360.Application.Features.Responsibles.Queries
{
    public record GetResponsibleByIdQuery(int Id) : IRequest<ResponsibleReadDto?>;
}