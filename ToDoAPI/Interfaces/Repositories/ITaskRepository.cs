using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task<TodoTask?> GetByIdAsync(Guid id, Guid userId, CancellationToken ct);
        Task<List<TodoTask>> GetAllAsync(Guid userId, CancellationToken ct);
        Task AddAsync(TodoTask task, CancellationToken ct);
        void Remove(TodoTask task);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
