using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(
            Guid id,
            Guid userId,
            CancellationToken ct);
        Task<List<Category>> GetAllAsync(
            Guid userId,
            CancellationToken ct);
        Task<bool> ExistsByNameAsync(
            Guid userId,
            string name,
            Guid? excludedCategoryId,
            CancellationToken ct);
        Task AddAsync(
            Category category,
            CancellationToken ct);
        void Remove(Category category);
        Task SaveChangesAsync(
            CancellationToken ct);
    }
}
