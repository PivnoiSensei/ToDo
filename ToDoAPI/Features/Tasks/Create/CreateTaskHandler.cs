using MediatR;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Features.Tasks.Common;
using ToDoAPI.Infrastructure.Exceptions;
using ToDoAPI.Interfaces.Repositories;
using ToDoAPI.Interfaces.Services;

namespace ToDoAPI.Features.Tasks.Create
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, TaskDto>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICategoryRepository _categoryRepository;

        public CreateTaskHandler(ITaskRepository taskRepository, ICurrentUserService currentUserService, ICategoryRepository categoryRepository)
        {
            _taskRepository = taskRepository;
            _currentUserService = currentUserService;
            _categoryRepository = categoryRepository;
        }

        public async Task<TaskDto> Handle(
            CreateTaskCommand req, CancellationToken ct
        )
        {
            var userId = _currentUserService.UserId;

            Category? category = null;

            if (req.CategoryId.HasValue)
            {
                category = await _categoryRepository.GetByIdAsync(
                    req.CategoryId.Value,
                    userId,
                    ct
                );

                if (category is null)
                {
                    throw new NotFoundException("Category not found");
                }
            }

            var task = TodoTask.Create(
              req.Title,
              req.Description,
              req.DueDate,
              userId,
              req.CategoryId
            );
            await _taskRepository.AddAsync(task, ct);
            await _taskRepository.SaveChangesAsync(ct);

            task.Category = category;

            return task.ToDto();
        }
    }
}
