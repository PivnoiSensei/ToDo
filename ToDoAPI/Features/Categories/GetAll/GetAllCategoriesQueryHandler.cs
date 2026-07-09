using MediatR;
using ToDoAPI.Features.Categories.Common;
using ToDoAPI.Interfaces.Repositories;
using ToDoAPI.Interfaces.Services;

namespace ToDoAPI.Features.Categories.GetAll
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetAllCategoriesQueryHandler(
            ICategoryRepository categoryRepository, 
            ICurrentUserService currentUserService)
        {
            _categoryRepository = categoryRepository;
            _currentUserService = currentUserService;
        }

        public async Task<List<CategoryDto>> Handle(GetAllCategoriesQuery req, CancellationToken ct)
        {
            var userId = _currentUserService.UserId;

            var categories = await _categoryRepository.GetAllAsync(
                userId,
                ct
            );

            return categories
                .Select(c => c.ToDto())
                .ToList();
        }
    }
}
