using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Audit360.Application.Features.Audits.Queries;
using Audit360.Application.Features.Dto.Audits;
using System.Collections.Generic;

namespace Audit360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AuditSummariesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuditSummariesController(IMediator mediator) => _mediator = mediator;

        [HttpGet("finalized")]
        public async Task<ActionResult<IEnumerable<AuditFinalizedSummaryReadDto>>> GetFinalized()
        {
            var result = await _mediator.Send(new GetAuditFinalizedSummaryQuery());
            return Ok(result);
        }
    }
}
