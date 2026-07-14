using MediatR;
using ToDoAPI.Domain.Entities;
using ToDoAPI.Features.Categories.Common;
using ToDoAPI.Infrastructure.Exceptions;
using ToDoAPI.Interfaces.Repositories;
using ToDoAPI.Interfaces.Services;

namespace ToDoAPI.Features.Categories.Create
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;

        public CreateCategoryHandler(
            ICategoryRepository categoryRepository, 
            ICurrentUserService currentUserService)
        {
            _categoryRepository = categoryRepository;
            _currentUserService = currentUserService;
        }

        public async Task<CategoryDto> Handle(CreateCategoryCommand req, CancellationToken ct)
        {
            var userId = _currentUserService.UserId;
            var catName = req.Name.Trim();

            if(await _categoryRepository.ExistsByNameAsync(
                userId,
                catName,
                null,
                ct))
            {
                throw new ConflictException("A category with this name already exists.");
            }

            var category = Category.Create(req.Name, userId);

            await _categoryRepository.AddAsync(category, ct);

            await _categoryRepository.SaveChangesAsync(ct);

            return category.ToDto();
        }
    }
}
