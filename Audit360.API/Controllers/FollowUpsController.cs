using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Audit360.Application.Features.FollowUps.Queries;
using Audit360.Application.Features.FollowUps.Commands;
using Audit360.Application.Features.Dto.FollowUps;

namespace Audit360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FollowUpsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FollowUpsController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Obtiene la lista de seguimientos.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FollowUpReadDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetFollowUpsQuery());
            return Ok(result);
        }

        /// <summary>
        /// Obtiene un seguimiento por su identificador.
        /// </summary>
        /// <param name="id">Identificador del seguimiento.</param>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<FollowUpReadDto?>> GetById(int id)
        {
            var result = await _mediator.Send(new GetFollowUpByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Crea un nuevo seguimiento.
        /// </summary>
        /// <param name="dto">Datos del seguimiento a crear.</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FollowUpWriteDto dto)
        {
            await _mediator.Send(new CreateFollowUpCommand(dto));
            return NoContent();
        }

        /// <summary>
        /// Actualiza un seguimiento existente.
        /// </summary>
        /// <param name="id">Identificador del seguimiento a actualizar.</param>
        /// <param name="dto">Datos actualizados del seguimiento.</param>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] FollowUpWriteDto dto)
        {
            await _mediator.Send(new UpdateFollowUpCommand(id, dto));
            return NoContent();
        }

        /// <summary>
        /// Elimina un seguimiento por su identificador.
        /// </summary>
        /// <param name="id">Identificador del seguimiento a eliminar.</param>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteFollowUpCommand(id));
            return NoContent();
        }
    }
}