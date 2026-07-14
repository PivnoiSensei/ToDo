using MediatR;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Infrastructure.Authentication;
using ToDoAPI.Infrastructure.Exceptions;
using ToDoAPI.Interfaces.Repositories;
using ToDoAPI.Interfaces.Services;

namespace ToDoAPI.Features.Auth.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtSettings _jwtSettings;

        public RegisterHandler(
           IUserRepository userRepository,
           IPasswordService passwordService,
           IJwtService jwtService,
           IHttpContextAccessor httpContextAccessor,
           Microsoft.Extensions.Options.IOptions<JwtSettings> jwtOptions)  
        {
           _userRepository = userRepository;
           _passwordService = passwordService;
           _jwtService = jwtService;
           _httpContextAccessor = httpContextAccessor;
           _jwtSettings = jwtOptions.Value;
        }

        public async Task<Unit> Handle(RegisterCommand req, CancellationToken ct)
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

            _httpContextAccessor.HttpContext!.Response.Cookies.Append(
               "access_token",
               token,
               new CookieOptions
               {
                   HttpOnly = true,
                   Secure = true,
                   SameSite = SameSiteMode.None,
                   Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes)
               });

            return Unit.Value;
        }
    }
}
