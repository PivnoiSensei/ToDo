namespace ToDoAPI.Features.Tasks.Common
{
    public sealed record TaskDto(
        Guid Id,
        string Title,
        string? Description,
        bool IsCompleted,
        DateTime? DueDate,
        Guid? CategoryId,
        string? CategoryName
    );
}
