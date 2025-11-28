using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Audit360.Application.Features.Statuses.Queries;
using Audit360.Application.Features.Statuses.Commands;
using Audit360.Application.Features.Dto.Statuses;

namespace Audit360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StatusesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StatusesController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Obtiene la lista de estados de auditoría.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditStatusReadDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAuditStatusesQuery());
            return Ok(result);
        }

        /// <summary>
        /// Obtiene un estado de auditoría por su identificador.
        /// </summary>
        /// <param name="id">Identificador del estado.</param>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AuditStatusReadDto?>> GetById(int id)
        {
            var result = await _mediator.Send(new GetAuditStatusByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Crea un nuevo estado de auditoría.
        /// </summary>
        /// <param name="dto">Datos del estado a crear.</param>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] AuditStatusWriteDto dto)
        {
            await _mediator.Send(new CreateAuditStatusCommand(dto));
            return NoContent();
        }

        /// <summary>
        /// Actualiza un estado existente.
        /// </summary>
        /// <param name="id">Identificador del estado a actualizar.</param>
        /// <param name="dto">Datos actualizados del estado.</param>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] AuditStatusWriteDto dto)
        {
            await _mediator.Send(new UpdateAuditStatusCommand(id, dto));
            return NoContent();
        }

        /// <summary>
        /// Elimina un estado por su identificador.
        /// </summary>
        /// <param name="id">Identificador del estado a eliminar.</param>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteAuditStatusCommand(id));
            return NoContent();
        }
    }
}