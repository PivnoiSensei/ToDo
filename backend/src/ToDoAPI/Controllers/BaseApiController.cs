using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ToDoAPI.Controllers {
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        protected readonly IMediator Mediator;

        protected BaseApiController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
