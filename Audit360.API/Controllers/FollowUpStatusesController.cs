using Microsoft.AspNetCore.Mvc;
using MediatR;
using Audit360.Application.Features.FollowUpStatuses.Queries;
using Audit360.Application.Features.FollowUpStatuses.Commands;
using Audit360.Application.Features.Dto.FollowUpStatuses;

namespace Audit360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FollowUpStatusesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FollowUpStatusesController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FollowUpStatusReadDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetFollowUpStatusesQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<FollowUpStatusReadDto?>> GetById(int id)
        {
            var result = await _mediator.Send(new GetFollowUpStatusByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FollowUpStatusWriteDto dto)
        {
            await _mediator.Send(new CreateFollowUpStatusCommand(dto));
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] FollowUpStatusWriteDto dto)
        {
            await _mediator.Send(new UpdateFollowUpStatusCommand(id, dto));
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteFollowUpStatusCommand(id));
            return NoContent();
        }
    }
}