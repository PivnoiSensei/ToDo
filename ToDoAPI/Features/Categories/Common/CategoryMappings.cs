using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Features.Categories.Common
{
    public static class CategoryMappings
    {
        public static CategoryDto ToDto(this Category category)
        {
            return new CategoryDto(
                category.Id,
                category.Name
            );
        }
    }
}
