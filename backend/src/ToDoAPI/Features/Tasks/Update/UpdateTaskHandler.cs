using Azure.Core;
using MediatR;
using ToDoAPI.Features.Tasks.Common;
using ToDoAPI.Infrastructure.Exceptions;
using ToDoAPI.Interfaces.Repositories;
using ToDoAPI.Interfaces.Services;

namespace ToDoAPI.Features.Tasks.Update
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, TaskDto>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateTaskHandler(
            ITaskRepository taskRepository,
            ICategoryRepository categoryRepository,
            ICurrentUserService currentUserService
        )
        {
            _taskRepository = taskRepository;
            _categoryRepository = categoryRepository;
            _currentUserService = currentUserService;
        }

        public async Task<TaskDto> Handle(
            UpdateTaskCommand req,
            CancellationToken ct
        )
        {
            var userId = _currentUserService.UserId;

            var task = await _taskRepository.GetByIdAsync(
                req.Id,
                userId,
                ct
            );

            if (task is null) {
                throw new NotFoundException("Task not found.");
            }

            if (req.CategoryId.HasValue)
            {
                var category = await _categoryRepository.GetByIdAsync(
                    req.CategoryId.Value,
                    userId,
                    ct
                );

                if (category is null)
                    throw new NotFoundException("Category not found.");

                task.Category = category;
            }
            else
            {
                task.Category = null;
            }

            task.Title = req.Title.Trim();
            task.Description = req.Description?.Trim();
            task.IsCompleted = req.IsCompleted;
            task.DueDate = req.DueDate;
            task.CategoryId = req.CategoryId;

            await _taskRepository.SaveChangesAsync(ct);

            return task.ToDto();
        }
    }
}
