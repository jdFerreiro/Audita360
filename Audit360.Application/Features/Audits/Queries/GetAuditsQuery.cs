using MediatR;
using System.Collections.Generic;
using Audit360.Application.Features.Dto.Audits;

namespace Audit360.Application.Features.Audits.Queries
{
    /// <summary>
    /// Query para obtener la lista de auditorías.
    /// </summary>
    public record GetAuditsQuery : IRequest<IEnumerable<AuditReadDto>>;
}