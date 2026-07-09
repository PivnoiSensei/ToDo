using FluentValidation;

namespace ToDoAPI.Features.Tasks.Create
{
    public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskValidator() {
            RuleFor(t => t.Title)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(t => t.Description)
                .MaximumLength(1000);
            RuleFor(t => t.DueDate)
                .GreaterThan(DateTime.UtcNow)
                .When(x => x.DueDate.HasValue);
        }
    }
}
