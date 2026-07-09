using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Common;
using ToDoAPI.Data;
using ToDoAPI.Features.Tasks.Common;
using ToDoAPI.Interfaces.Services;

namespace ToDoAPI.Features.Tasks.GetAll;

public class GetAllTasksQueryHandler
    : IRequestHandler<GetAllTasksQuery, PagedResult<TaskDto>>
{
    private readonly AppDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetAllTasksQueryHandler(
        AppDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<PagedResult<TaskDto>> Handle(
        GetAllTasksQuery request,
        CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        var query = _context.TodoTasks
            .AsNoTracking()
            .Include(t => t.Category)
            .Where(t => t.UserId == userId);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = $"%{request.Search.Trim()}%";

            query = query.Where(t =>
                EF.Functions.Like(t.Title, search) ||
                (t.Description != null &&
                 EF.Functions.Like(t.Description, search)));
        }

        if (request.CategoryId.HasValue)
        {
            query = query.Where(t =>
                t.CategoryId == request.CategoryId);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var tasks = await query
            .OrderByDescending(t => t.CreatedAt)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<TaskDto>(
            tasks.Select(t => t.ToDto()).ToList(),
            request.Page,
            request.PageSize,
            totalCount);
    }
}