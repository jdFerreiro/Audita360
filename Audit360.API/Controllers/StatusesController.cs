using Microsoft.AspNetCore.Mvc;
using MediatR;
using Audit360.Application.Features.Statuses.Queries;
using Audit360.Application.Features.Statuses.Commands;
using Audit360.Application.Features.Dto.Statuses;

namespace Audit360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StatusesController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditStatusReadDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAuditStatusesQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AuditStatusReadDto?>> GetById(int id)
        {
            var result = await _mediator.Send(new GetAuditStatusByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuditStatusWriteDto dto)
        {
            await _mediator.Send(new CreateAuditStatusCommand(dto));
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] AuditStatusWriteDto dto)
        {
            await _mediator.Send(new UpdateAuditStatusCommand(id, dto));
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteAuditStatusCommand(id));
            return NoContent();
        }
    }
}