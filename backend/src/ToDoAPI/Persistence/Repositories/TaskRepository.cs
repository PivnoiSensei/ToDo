using Microsoft.EntityFrameworkCore;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Interfaces.Repositories;

namespace ToDoAPI.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TodoTask?> GetByIdAsync(
            Guid id,
            Guid userId,
            CancellationToken ct)
        {
            return await _context.TodoTasks
                .Include(t => t.Category)
                .FirstOrDefaultAsync(
                    t => t.Id == id && t.UserId == userId,
                    ct);
        }

        public async Task<List<TodoTask>> GetAllAsync(
            Guid userId,
            CancellationToken ct)
        {
            return await _context.TodoTasks
                .Include(t => t.Category)
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync(ct);
        }

        public async Task AddAsync(
            TodoTask todoTask,
            CancellationToken ct)
        {
            await _context.TodoTasks.AddAsync(todoTask, ct);
        }

        public void Remove(TodoTask todoTask)
        {
            _context.TodoTasks.Remove(todoTask);
        }

        public async Task SaveChangesAsync(
            CancellationToken ct)
        {
            await _context.SaveChangesAsync(ct);
        }
    }
}
