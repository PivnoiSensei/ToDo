using MediatR;

namespace ToDoAPI.Features.Categories.Delete
{
    public sealed record DeleteCategoryCommand(
        Guid Id
    ) : IRequest;
}
