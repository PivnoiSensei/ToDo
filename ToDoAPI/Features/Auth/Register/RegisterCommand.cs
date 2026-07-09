using MediatR;
using ToDoAPI.Features.Auth.Common;

namespace ToDoAPI.Features.Auth.Register
{
    public sealed record RegisterCommand(
        string Email,
        string Password
    ) : IRequest<AuthResponse>;
}
