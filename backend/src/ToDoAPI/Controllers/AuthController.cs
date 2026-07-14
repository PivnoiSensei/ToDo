using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> Register(
            RegisterCommand command,
            CancellationToken ct)
        {
            await Mediator.Send(command, ct);

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(
            LoginCommand command,
            CancellationToken ct)
        {
            await Mediator.Send(command, ct);

            return NoContent();
        }

        [Authorize]
        [HttpGet("check")]
        public IActionResult Check()
        {
            return NoContent();
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("access_token", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/"
            });

            return NoContent();
        }
    }
}
