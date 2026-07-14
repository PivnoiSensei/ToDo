using MediatR;
using ToDoAPI.Features.Tasks.Common;

namespace ToDoAPI.Features.Tasks.Update
{
    public sealed record UpdateTaskCommand(
        Guid Id,
        string Title,
        string? Description,
        bool IsCompleted,
        DateTime? DueDate,
        Guid? CategoryId
    ) : IRequest<TaskDto>;
}
