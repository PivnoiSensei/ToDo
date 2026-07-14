using MediatR;

namespace ToDoAPI.Features.Tasks.Delete
{
    public sealed record DeleteTaskCommand(Guid id) : IRequest;
}
