using MediatR;
using ToDoAPI.Features.Categories.Common;

namespace ToDoAPI.Features.Categories.Update
{
    public sealed record UpdateCategoryCommand(
        Guid Id,
        string Name
    ) : IRequest<CategoryDto>;
}
