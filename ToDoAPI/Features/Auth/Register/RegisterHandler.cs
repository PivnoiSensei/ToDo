using MediatR;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Features.Auth.Common;
using ToDoAPI.Infrastructure.Exceptions;
using ToDoAPI.Interfaces.Repositories;
using ToDoAPI.Interfaces.Services;

namespace ToDoAPI.Features.Auth.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;

        public RegisterHandler(
           IUserRepository userRepository,
           IPasswordService passwordService,
           IJwtService jwtService)  
        {
           _userRepository = userRepository;
           _passwordService = passwordService;
           _jwtService = jwtService;
        }

        public async Task<AuthResponse> Handle(RegisterCommand req, CancellationToken ct)
        {
            if(await _userRepository.ExistsByEmailAsync(req.Email))
            {
                throw new ConflictException("A user with this email already exists.");
            }

            var passwordHash = _passwordService.Hash(req.Password);

            var user = User.Create(
                req.Email,
                passwordHash);

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            var token = _jwtService.GenerateToken(user);

            return new AuthResponse(
                user.Id,
                user.Email,
                token);
        }
    }
}
