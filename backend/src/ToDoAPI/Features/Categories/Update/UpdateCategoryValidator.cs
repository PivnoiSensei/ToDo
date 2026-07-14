using FluentValidation;

namespace ToDoAPI.Features.Categories.Update
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryValidator() { 
            RuleFor(c => c.Name)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(50);
        }
    }
}
