using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Features.Tasks.Common
{
    public static class TaskMappings
    {
        public static TaskDto ToDto(this TodoTask task)
        {
            return new TaskDto(
                task.Id,
                task.Title,
                task.Description,
                task.IsCompleted,
                task.DueDate,
                task.CategoryId,
                task.Category?.Name
            );
        }
    }
}
