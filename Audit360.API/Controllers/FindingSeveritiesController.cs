using Microsoft.AspNetCore.Mvc;
using MediatR;
using Audit360.Application.Features.FindingSeverities.Queries;
using Audit360.Application.Features.FindingSeverities.Commands;
using Audit360.Application.Features.Dto.FindingSeverities;

namespace Audit360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FindingSeveritiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FindingSeveritiesController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FindingSeverityReadDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetFindingSeveritiesQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<FindingSeverityReadDto?>> GetById(int id)
        {
            var result = await _mediator.Send(new GetFindingSeverityByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FindingSeverityWriteDto dto)
        {
            await _mediator.Send(new CreateFindingSeverityCommand(dto));
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] FindingSeverityWriteDto dto)
        {
            await _mediator.Send(new UpdateFindingSeverityCommand(id, dto));
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteFindingSeverityCommand(id));
            return NoContent();
        }
    }
}