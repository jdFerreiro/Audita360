using Microsoft.AspNetCore.Mvc;
using MediatR;
using Audit360.Application.Features.FollowUps.Queries;
using Audit360.Application.Features.FollowUps.Commands;
using Audit360.Application.Features.Dto.FollowUps;

namespace Audit360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FollowUpsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FollowUpsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FollowUpReadDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetFollowUpsQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<FollowUpReadDto?>> GetById(int id)
        {
            var result = await _mediator.Send(new GetFollowUpByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FollowUpWriteDto dto)
        {
            await _mediator.Send(new CreateFollowUpCommand(dto));
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] FollowUpWriteDto dto)
        {
            await _mediator.Send(new UpdateFollowUpCommand(id, dto));
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteFollowUpCommand(id));
            return NoContent();
        }
    }
}