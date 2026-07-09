using MediatR;
using ToDoAPI.Features.Auth.Common;

namespace ToDoAPI.Features.Auth.Login
{
    public sealed record LoginCommand(
        string Email,
        string Password
    ) : IRequest<AuthResponse>;
}
