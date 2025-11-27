using Microsoft.AspNetCore.Mvc;
using MediatR;
using Audit360.Application.Features.Findings.Queries;
using Audit360.Application.Features.Findings.Commands;
using Audit360.Application.Features.Dto.Findings;

namespace Audit360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FindingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FindingsController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Obtiene la lista de hallazgos.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FindingReadDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetFindingsQuery());
            return Ok(result);
        }

        /// <summary>
        /// Obtiene un hallazgo por su identificador.
        /// </summary>
        /// <param name="id">Identificador del hallazgo.</param>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<FindingReadDto?>> GetById(int id)
        {
            var result = await _mediator.Send(new GetFindingByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Crea un nuevo hallazgo.
        /// </summary>
        /// <param name="dto">Datos del hallazgo a crear.</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FindingWriteDto dto)
        {
            await _mediator.Send(new CreateFindingCommand(dto));
            return NoContent();
        }

        /// <summary>
        /// Actualiza un hallazgo existente.
        /// </summary>
        /// <param name="id">Identificador del hallazgo a actualizar.</param>
        /// <param name="dto">Datos actualizados del hallazgo.</param>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] FindingWriteDto dto)
        {
            await _mediator.Send(new UpdateFindingCommand(id, dto));
            return NoContent();
        }

        /// <summary>
        /// Elimina un hallazgo por su identificador.
        /// </summary>
        /// <param name="id">Identificador del hallazgo a eliminar.</param>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteFindingCommand(id));
            return NoContent();
        }
    }
}