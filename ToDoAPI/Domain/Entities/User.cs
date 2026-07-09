namespace ToDoAPI.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public ICollection<TodoTask> Tasks { get; set; } = [];
        public ICollection<Category> Categories { get; set; } = [];

        public static User Create(string email, string passwordHash)
        {
            return new User
            {
                Email = email,
                PasswordHash = passwordHash
            };
        }
    }
}
