using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Audit360.Application.Features.FollowUpStatuses.Queries;
using Audit360.Application.Features.FollowUpStatuses.Commands;
using Audit360.Application.Features.Dto.FollowUpStatuses;

namespace Audit360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FollowUpStatusesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FollowUpStatusesController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Obtiene la lista de estados de seguimiento.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FollowUpStatusReadDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetFollowUpStatusesQuery());
            return Ok(result);
        }

        /// <summary>
        /// Obtiene un estado de seguimiento por su identificador.
        /// </summary>
        /// <param name="id">Identificador del estado.</param>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<FollowUpStatusReadDto?>> GetById(int id)
        {
            var result = await _mediator.Send(new GetFollowUpStatusByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Crea un nuevo estado de seguimiento.
        /// </summary>
        /// <param name="dto">Datos del estado a crear.</param>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] FollowUpStatusWriteDto dto)
        {
            await _mediator.Send(new CreateFollowUpStatusCommand(dto));
            return NoContent();
        }

        /// <summary>
        /// Actualiza un estado de seguimiento existente.
        /// </summary>
        /// <param name="id">Identificador del estado a actualizar.</param>
        /// <param name="dto">Datos actualizados del estado.</param>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] FollowUpStatusWriteDto dto)
        {
            await _mediator.Send(new UpdateFollowUpStatusCommand(id, dto));
            return NoContent();
        }

        /// <summary>
        /// Elimina un estado de seguimiento por su identificador.
        /// </summary>
        /// <param name="id">Identificador del estado a eliminar.</param>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteFollowUpStatusCommand(id));
            return NoContent();
        }
    }
}