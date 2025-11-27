using Microsoft.AspNetCore.Mvc;
using MediatR;
using Audit360.Application.Features.Audits.Queries;
using Audit360.Application.Features.Audits.Commands;
using Audit360.Application.Features.Dto.Audits;

namespace Audit360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuditsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditReadDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAuditsQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AuditReadDto?>> GetById(int id)
        {
            var result = await _mediator.Send(new GetAuditByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuditWriteDto dto)
        {
            await _mediator.Send(new CreateAuditCommand(dto));
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] AuditWriteDto dto)
        {
            await _mediator.Send(new UpdateAuditCommand(id, dto));
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteAuditCommand(id));
            return NoContent();
        }
    }
}