using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Audit360.Application.Features.FindingTypes.Queries;
using Audit360.Application.Features.FindingTypes.Commands;
using Audit360.Application.Features.Dto.FindingTypes;

namespace Audit360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FindingTypesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FindingTypesController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Obtiene la lista de tipos de hallazgo.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FindingTypeReadDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetFindingTypesQuery());
            return Ok(result);
        }

        /// <summary>
        /// Obtiene un tipo de hallazgo por su identificador.
        /// </summary>
        /// <param name="id">Identificador del tipo.</param>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<FindingTypeReadDto?>> GetById(int id)
        {
            var result = await _mediator.Send(new GetFindingTypeByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Crea un nuevo tipo de hallazgo.
        /// </summary>
        /// <param name="dto">Datos del tipo a crear.</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FindingTypeWriteDto dto)
        {
            await _mediator.Send(new CreateFindingTypeCommand(dto));
            return NoContent();
        }

        /// <summary>
        /// Actualiza un tipo de hallazgo existente.
        /// </summary>
        /// <param name="id">Identificador del tipo a actualizar.</param>
        /// <param name="dto">Datos actualizados del tipo.</param>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] FindingTypeWriteDto dto)
        {
            await _mediator.Send(new UpdateFindingTypeCommand(id, dto));
            return NoContent();
        }

        /// <summary>
        /// Elimina un tipo por su identificador.
        /// </summary>
        /// <param name="id">Identificador del tipo a eliminar.</param>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteFindingTypeCommand(id));
            return NoContent();
        }
    }
}