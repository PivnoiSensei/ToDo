namespace ToDoAPI.Domain.Entities
{
    public class TodoTask : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }

        public static TodoTask Create(
            string title,
            string? description,
            DateTime? dueDate,
            Guid userId,
            Guid? categoryId
        )
        {
            return new TodoTask
            {
                Title = title,
                Description = description,
                DueDate = dueDate,
                UserId = userId,
                CategoryId = categoryId
            };
        }
    }
}
