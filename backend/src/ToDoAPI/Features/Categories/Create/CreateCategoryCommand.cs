using MediatR;
using ToDoAPI.Features.Categories.Common;

namespace ToDoAPI.Features.Categories.Create
{
    public sealed record CreateCategoryCommand(
       string Name
    ) : IRequest<CategoryDto>;
}
