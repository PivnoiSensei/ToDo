using MediatR;
using ToDoAPI.Features.Categories.Common;

namespace ToDoAPI.Features.Categories.GetAll
{
    public sealed record GetAllCategoriesQuery :
          IRequest<List<CategoryDto>>;
}
