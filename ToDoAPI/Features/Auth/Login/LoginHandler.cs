using MediatR;
using ToDoAPI.Features.Auth.Common;
using ToDoAPI.Infrastructure.Exceptions;
using ToDoAPI.Interfaces.Repositories;
using ToDoAPI.Interfaces.Services;

namespace ToDoAPI.Features.Auth.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;

        public LoginHandler(
            IUserRepository userRepository, 
            IPasswordService passwordService, 
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse> Handle (LoginCommand req, CancellationToken ct)
        {
            //User is possibly Null here
            var user = await _userRepository.GetByEmailAsync(req.Email) ?? throw new UnauthorizedAccessException("Incorrect email or password");

            if(!_passwordService.Verify(req.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Incorrect email or password");

            var token = _jwtService.GenerateToken(user);
            return new AuthResponse(user.Id, user.Email, token);
        }
    }
}
