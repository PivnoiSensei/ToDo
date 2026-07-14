using System.Security.Claims;
using ToDoAPI.Interfaces.Services;

namespace ToDoAPI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated =>
            _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;

        public Guid UserId
        {
            get
            {
                var value = _httpContextAccessor.HttpContext?
                    .User
                    .FindFirstValue(ClaimTypes.NameIdentifier);

                if (value is null)
                    throw new UnauthorizedAccessException();

                if (!Guid.TryParse(value, out var userId))
                {
                    throw new UnauthorizedAccessException();
                }

                return userId;
            }
        }
    }
}
