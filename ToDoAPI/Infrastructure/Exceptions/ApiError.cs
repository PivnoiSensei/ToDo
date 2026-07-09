namespace ToDoAPI.Infrastructure.Exceptions
{
    public sealed record ApiError(
    int StatusCode,
    string Message,
    string? TraceId = null,
    IReadOnlyCollection<string>? Errors = null);
}
