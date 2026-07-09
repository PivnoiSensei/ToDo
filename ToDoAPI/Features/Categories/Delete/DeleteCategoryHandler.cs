using MediatR;
using ToDoAPI.Infrastructure.Exceptions;
using ToDoAPI.Interfaces.Repositories;
using ToDoAPI.Interfaces.Services;

namespace ToDoAPI.Features.Categories.Delete
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteCategoryHandler(ICategoryRepository categoryRepository, ICurrentUserService currentUserService)
        {
            _categoryRepository = categoryRepository;
            _currentUserService = currentUserService;
        }

        public async Task Handle(DeleteCategoryCommand req, CancellationToken ct) {
            var userId = _currentUserService.UserId;

            var category = await _categoryRepository.GetByIdAsync(req.Id, userId, ct);

            if (category is null) {
                throw new NotFoundException("Category not found");
            }

            _categoryRepository.Remove(category);

            await _categoryRepository.SaveChangesAsync(ct);
        }
    }
}
