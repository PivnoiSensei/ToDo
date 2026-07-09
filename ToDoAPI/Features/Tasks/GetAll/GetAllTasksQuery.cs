using MediatR;
using ToDoAPI.Common;
using ToDoAPI.Features.Tasks.Common;

namespace ToDoAPI.Features.Tasks.GetAll
{
    public sealed record GetAllTasksQuery(
        int Page = 1,
        int PageSize = 10,
        string? Search = null,
        Guid? CategoryId = null
    ) : IRequest<PagedResult<TaskDto>>; 
}
