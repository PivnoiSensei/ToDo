using MediatR;
using ToDoAPI.Infrastructure.Authentication;
using ToDoAPI.Interfaces.Repositories;
using ToDoAPI.Interfaces.Services;

namespace ToDoAPI.Features.Auth.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtSettings _jwtSettings;

        public LoginHandler(
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

        public async Task<Unit> Handle (LoginCommand req, CancellationToken ct)
        {
            //User is possibly Null here
            var user = await _userRepository.GetByEmailAsync(req.Email) ?? throw new UnauthorizedAccessException("Incorrect email or password");

            if(!_passwordService.Verify(req.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Incorrect email or password");

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
