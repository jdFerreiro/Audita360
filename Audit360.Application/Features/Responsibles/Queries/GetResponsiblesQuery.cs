using MediatR;
using System.Collections.Generic;
using Audit360.Application.Features.Dto.Responsibles;

namespace Audit360.Application.Features.Responsibles.Queries
{
    public record GetResponsiblesQuery : IRequest<IEnumerable<ResponsibleReadDto>>;
}