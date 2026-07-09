using MediatR;
using ToDoAPI.Infrastructure.Exceptions;
using ToDoAPI.Interfaces.Repositories;
using ToDoAPI.Interfaces.Services;

namespace ToDoAPI.Features.Tasks.Delete
{
    public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteTaskHandler(
            ITaskRepository taskRepository,
            ICurrentUserService currentUserService)
        {
            _taskRepository = taskRepository;
            _currentUserService = currentUserService;
        }

        public async Task Handle(DeleteTaskCommand req, CancellationToken ct) {
            var userId = _currentUserService.UserId;

            var task = await _taskRepository.GetByIdAsync(req.id, userId, ct);

            if(task is null)
            {
                throw new NotFoundException("Task not found.");
            }

            _taskRepository.Remove(task);

            await _taskRepository.SaveChangesAsync(ct);
        }
    }
}
