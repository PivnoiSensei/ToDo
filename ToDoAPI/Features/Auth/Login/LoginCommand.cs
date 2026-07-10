using MediatR;

namespace ToDoAPI.Features.Auth.Login
{
    public sealed record LoginCommand(
        string Email,
        string Password
    ) : IRequest<Unit>;
}
