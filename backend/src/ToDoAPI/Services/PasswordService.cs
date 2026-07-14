using Microsoft.AspNetCore.Identity;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Interfaces.Services;

namespace ToDoAPI.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordHasher<User> _hasher = new();

        public string Hash(string password)
        {
            return _hasher.HashPassword(null!, password);
        }
        public bool Verify(string password, string passwordHash)
        {
            var res = _hasher.VerifyHashedPassword(
                null!,
                passwordHash,
                password
            );

            return res != PasswordVerificationResult.Failed;
        }
    }
}
