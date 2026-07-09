namespace ToDoAPI.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public ICollection<TodoTask> Tasks { get; set; } = [];

        public static Category Create(string name, Guid userId) { 
            return new Category { Name = name, UserId = userId };
        }
    }
}
