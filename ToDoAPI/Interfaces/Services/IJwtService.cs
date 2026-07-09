using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);       
    }
}
