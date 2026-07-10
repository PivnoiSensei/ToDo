using MediatR;

namespace ToDoAPI.Features.Auth.Register
{
    public sealed record RegisterCommand(
        string Email,
        string Password
    ) : IRequest<Unit>;
}
