using Microsoft.AspNetCore.Mvc;
using MediatR;
using Audit360.Application.Features.Roles.Queries;
using Audit360.Application.Features.Roles.Commands;
using Audit360.Application.Features.Dto.Roles;

namespace Audit360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RolesController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Obtiene la lista de roles.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleReadDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetRolesQuery());
            return Ok(result);
        }

        /// <summary>
        /// Obtiene un rol por su identificador.
        /// </summary>
        /// <param name="id">Identificador del rol.</param>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<RoleReadDto?>> GetById(int id)
        {
            var result = await _mediator.Send(new GetRoleByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Crea un nuevo rol.
        /// </summary>
        /// <param name="dto">Datos del rol a crear.</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleWriteDto dto)
        {
            await _mediator.Send(new CreateRoleCommand(dto));
            return NoContent();
        }

        /// <summary>
        /// Actualiza un rol existente.
        /// </summary>
        /// <param name="id">Identificador del rol a actualizar.</param>
        /// <param name="dto">Datos actualizados del rol.</param>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] RoleWriteDto dto)
        {
            await _mediator.Send(new UpdateRoleCommand(id, dto));
            return NoContent();
        }

        /// <summary>
        /// Elimina un rol por su identificador.
        /// </summary>
        /// <param name="id">Identificador del rol a eliminar.</param>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteRoleCommand(id));
            return NoContent();
        }
    }
}