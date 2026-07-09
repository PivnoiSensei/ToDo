using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Features.Tasks.Common;
using ToDoAPI.Features.Tasks.Create;
using ToDoAPI.Features.Tasks.Delete;
using ToDoAPI.Features.Tasks.GetAll;
using ToDoAPI.Features.Tasks.Update;

namespace ToDoAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/tasks")]
    public class TasksController : BaseApiController
    {
        public TasksController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateTaskCommand command,
            CancellationToken ct
        )
        { 
            return Ok(await Mediator.Send(command, ct));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllTasksQuery query,
            CancellationToken ct)
        {
            return Ok(await Mediator.Send(query, ct));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            UpdateTaskCommand command,
            CancellationToken ct
        )
        {
            return Ok(await Mediator.Send(command with { Id = id }, ct));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken ct
        )
        {
            await Mediator.Send(new DeleteTaskCommand(id), ct);

            return NoContent();
        }
    }
}
