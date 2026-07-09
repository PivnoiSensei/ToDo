using MediatR;
using ToDoAPI.Features.Categories.Common;
using ToDoAPI.Infrastructure.Exceptions;
using ToDoAPI.Interfaces.Repositories;
using ToDoAPI.Interfaces.Services;

namespace ToDoAPI.Features.Categories.Update
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateCategoryHandler(
            ICategoryRepository categoryRepository,
            ICurrentUserService currentUserService)
        {
            _categoryRepository = categoryRepository;
            _currentUserService = currentUserService;
        }

        public async Task<CategoryDto> Handle(UpdateCategoryCommand req, CancellationToken ct) 
        {
            var userId = _currentUserService.UserId;

            var category = await _categoryRepository.GetByIdAsync(
                req.Id,
                userId,
                ct);

            if (category is null)
                throw new NotFoundException("Category not found");

            var catName = req.Name.Trim();

            var exists = await _categoryRepository.ExistsByNameAsync(
               userId,
               catName,
               req.Id,
               ct);

            if (exists)
            {
                throw new ConflictException("A category with this name already exists.");
            }

            category.Name = catName;

            await _categoryRepository.SaveChangesAsync(ct);

            return category.ToDto();
        }
    }
}
