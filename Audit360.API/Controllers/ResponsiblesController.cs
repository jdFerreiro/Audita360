using Microsoft.AspNetCore.Mvc;
using MediatR;
using Audit360.Application.Features.Responsibles.Queries;
using Audit360.Application.Features.Responsibles.Commands;
using Audit360.Application.Features.Dto.Responsibles;

namespace Audit360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResponsiblesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ResponsiblesController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Obtiene la lista de responsables.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponsibleReadDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetResponsiblesQuery());
            return Ok(result);
        }

        /// <summary>
        /// Obtiene un responsable por su identificador.
        /// </summary>
        /// <param name="id">Identificador del responsable.</param>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResponsibleReadDto?>> GetById(int id)
        {
            var result = await _mediator.Send(new GetResponsibleByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Crea un nuevo responsable.
        /// </summary>
        /// <param name="dto">Datos del responsable a crear.</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ResponsibleWriteDto dto)
        {
            await _mediator.Send(new CreateResponsibleCommand(dto));
            return NoContent();
        }

        /// <summary>
        /// Actualiza un responsable existente.
        /// </summary>
        /// <param name="id">Identificador del responsable a actualizar.</param>
        /// <param name="dto">Datos actualizados del responsable.</param>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ResponsibleWriteDto dto)
        {
            await _mediator.Send(new UpdateResponsibleCommand(id, dto));
            return NoContent();
        }

        /// <summary>
        /// Elimina un responsable por su identificador.
        /// </summary>
        /// <param name="id">Identificador del responsable a eliminar.</param>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteResponsibleCommand(id));
            return NoContent();
        }
    }
}