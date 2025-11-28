using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Audit360.Application.Features.FindingSeverities.Queries;
using Audit360.Application.Features.FindingSeverities.Commands;
using Audit360.Application.Features.Dto.FindingSeverities;

namespace Audit360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FindingSeveritiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FindingSeveritiesController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Obtiene la lista de severidades de hallazgos.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FindingSeverityReadDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetFindingSeveritiesQuery());
            return Ok(result);
        }

        /// <summary>
        /// Obtiene una severidad por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la severidad.</param>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<FindingSeverityReadDto?>> GetById(int id)
        {
            var result = await _mediator.Send(new GetFindingSeverityByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Crea una nueva severidad de hallazgo.
        /// </summary>
        /// <param name="dto">Datos de la severidad a crear.</param>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] FindingSeverityWriteDto dto)
        {
            await _mediator.Send(new CreateFindingSeverityCommand(dto));
            return NoContent();
        }

        /// <summary>
        /// Actualiza una severidad existente.
        /// </summary>
        /// <param name="id">Identificador de la severidad a actualizar.</param>
        /// <param name="dto">Datos actualizados de la severidad.</param>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] FindingSeverityWriteDto dto)
        {
            await _mediator.Send(new UpdateFindingSeverityCommand(id, dto));
            return NoContent();
        }

        /// <summary>
        /// Elimina una severidad por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la severidad a eliminar.</param>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteFindingSeverityCommand(id));
            return NoContent();
        }
    }
}