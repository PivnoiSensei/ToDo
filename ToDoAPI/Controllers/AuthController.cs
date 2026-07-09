using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Features.Auth.Common;
using ToDoAPI.Features.Auth.Login;
using ToDoAPI.Features.Auth.Register;

namespace ToDoAPI.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : BaseApiController
    {
        public AuthController(IMediator mediator) : base(mediator) {}

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(
            RegisterCommand command,
            CancellationToken ct)
        {
            return Ok(await Mediator.Send(command, ct));
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(
            LoginCommand command,
            CancellationToken ct)
        {
            return Ok(await Mediator.Send(command, ct));
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Append("ClearJwt", "", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(-1)
            });
            return NoContent();
        }
    }
}
