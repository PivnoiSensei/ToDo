using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Features.Categories.Common;
using ToDoAPI.Features.Categories.Create;
using ToDoAPI.Features.Categories.Delete;
using ToDoAPI.Features.Categories.GetAll;
using ToDoAPI.Features.Categories.Update;

namespace ToDoAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/categories")]
    public class CategoriesController : BaseApiController
    {
        public CategoriesController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateCategoryCommand command,
            CancellationToken ct
        )
        {
            return Ok(await Mediator.Send(command, ct));
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAll(CancellationToken ct)
        {
            return Ok(await Mediator.Send(
                new GetAllCategoriesQuery(),
                ct
            ));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            UpdateCategoryCommand command,
            CancellationToken ct)
        {
            command = command with { Id = id };

            return Ok(await Mediator.Send(command, ct));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken ct)
        {
            await Mediator.Send(new DeleteCategoryCommand(id), ct);
            return NoContent();
        }
    }
}
