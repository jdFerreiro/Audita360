using Microsoft.AspNetCore.Mvc;
using MediatR;
using Audit360.Application.Features.Users.Commands;
using Audit360.Application.Features.Dto.Users;

namespace Audit360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Autenticación: recibe email y password, devuelve JWT.
        /// </summary>
        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticateResponseDto>> Authenticate([FromBody] AuthenticateRequestDto dto)
        {
            var result = await _mediator.Send(new AuthenticateUserCommand(dto.Email, dto.Password));
            if (result == null) return Unauthorized();
            return Ok(result);
        }
    }
}
