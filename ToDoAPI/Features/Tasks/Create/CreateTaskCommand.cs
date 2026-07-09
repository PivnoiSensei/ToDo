using MediatR;
using ToDoAPI.Features.Tasks.Common;

namespace ToDoAPI.Features.Tasks.Create
{
    public sealed record CreateTaskCommand(
        string Title,
        string? Description,
        DateTime? DueDate,
        Guid? CategoryId
    ) : IRequest<TaskDto>;
 
}
