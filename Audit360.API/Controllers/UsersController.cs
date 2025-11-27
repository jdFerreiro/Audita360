using Audit360.Application.Features.Dto.Users;
using Audit360.Application.Features.Users.Commands;
using Audit360.Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Audit360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Obtiene la lista de usuarios.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetUsersQuery());
            return Ok(result);
        }

        /// <summary>
        /// Obtiene un usuario por su identificador.
        /// </summary>
        /// <param name="id">Identificador del usuario.</param>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserReadDto?>> GetById(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Crea un nuevo usuario.
        /// </summary>
        /// <param name="dto">Datos del usuario a crear.</param>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] UserWriteDto dto)
        {
            await _mediator.Send(new CreateUserCommand(dto));
            return NoContent();
        }

        /// <summary>
        /// Actualiza un usuario existente.
        /// </summary>
        /// <param name="id">Identificador del usuario a actualizar.</param>
        /// <param name="dto">Datos actualizados del usuario.</param>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserWriteDto dto)
        {
            await _mediator.Send(new UpdateUserCommand(id, dto));
            return NoContent();
        }

        /// <summary>
        /// Elimina un usuario por su identificador.
        /// </summary>
        /// <param name="id">Identificador del usuario a eliminar.</param>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteUserCommand(id));
            return NoContent();
        }
    }
}