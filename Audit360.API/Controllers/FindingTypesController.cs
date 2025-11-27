using Microsoft.AspNetCore.Mvc;
using MediatR;
using Audit360.Application.Features.FindingTypes.Queries;
using Audit360.Application.Features.FindingTypes.Commands;
using Audit360.Application.Features.Dto.FindingTypes;

namespace Audit360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FindingTypesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FindingTypesController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FindingTypeReadDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetFindingTypesQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<FindingTypeReadDto?>> GetById(int id)
        {
            var result = await _mediator.Send(new GetFindingTypeByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FindingTypeWriteDto dto)
        {
            await _mediator.Send(new CreateFindingTypeCommand(dto));
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] FindingTypeWriteDto dto)
        {
            await _mediator.Send(new UpdateFindingTypeCommand(id, dto));
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteFindingTypeCommand(id));
            return NoContent();
        }
    }
}