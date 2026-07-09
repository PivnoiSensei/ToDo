using Microsoft.EntityFrameworkCore;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Interfaces.Repositories;

namespace ToDoAPI.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Category?> GetByIdAsync(
            Guid id,
            Guid userId,
            CancellationToken ct)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(
                    c => c.Id == id && c.UserId == userId,
                    ct);
        }

        public async Task<List<Category>> GetAllAsync(
            Guid userId,
            CancellationToken ct)
        {
            return await _context.Categories
                .Where(c => c.UserId == userId)
                .OrderBy(c => c.Name)
                .ToListAsync(ct);
        }

        public async Task<bool> ExistsByNameAsync(
            Guid userId,
            string name,
            Guid? excludedCategoryId,
            CancellationToken ct)
        {
            return await _context.Categories.AnyAsync(
             c =>
                 c.UserId == userId &&
                 c.Name == name &&
                 c.Id != excludedCategoryId,
             ct);
        }

        public async Task AddAsync(
            Category category,
            CancellationToken ct)
        {
            await _context.Categories.AddAsync(category, ct);
        }

        public void Remove(Category category)
        {
            _context.Categories.Remove(category);
        }

        public async Task SaveChangesAsync(
            CancellationToken ct)
        {
            await _context.SaveChangesAsync(ct);
        }
    }
}