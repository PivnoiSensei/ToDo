using FluentValidation;

namespace ToDoAPI.Features.Categories.Create
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator() { 
            RuleFor(c => c.Name)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(50);
        }
    }
}
