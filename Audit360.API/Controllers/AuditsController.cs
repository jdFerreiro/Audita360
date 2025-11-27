using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Audit360.Application.Features.Audits.Queries;
using Audit360.Application.Features.Audits.Commands;
using Audit360.Application.Features.Dto.Audits;

namespace Audit360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AuditsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuditsController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Obtiene la lista de auditorías.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditReadDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAuditsQuery());
            return Ok(result);
        }

        /// <summary>
        /// Obtiene una auditoría por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la auditoría.</param>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AuditReadDto?>> GetById(int id)
        {
            var result = await _mediator.Send(new GetAuditByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Crea una nueva auditoría.
        /// </summary>
        /// <param name="dto">Datos de la auditoría a crear.</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuditWriteDto dto)
        {
            await _mediator.Send(new CreateAuditCommand(dto));
            return NoContent();
        }

        /// <summary>
        /// Actualiza una auditoría existente.
        /// </summary>
        /// <param name="id">Identificador de la auditoría a actualizar.</param>
        /// <param name="dto">Datos actualizados de la auditoría.</param>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] AuditWriteDto dto)
        {
            await _mediator.Send(new UpdateAuditCommand(id, dto));
            return NoContent();
        }

        /// <summary>
        /// Elimina una auditoría por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la auditoría a eliminar.</param>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteAuditCommand(id));
            return NoContent();
        }
    }
}