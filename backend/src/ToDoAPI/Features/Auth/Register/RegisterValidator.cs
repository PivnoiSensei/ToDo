using FluentValidation;

namespace ToDoAPI.Features.Auth.Register
{
    public class RegisterValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email)
              .NotEmpty()
              .WithMessage("Email is required.")
              .EmailAddress()
              .WithMessage("Invalid email format.")
              .MaximumLength(255);

            RuleFor(x => x.Password)
              .NotEmpty()
              .WithMessage("Password is required.")
              .MinimumLength(8)
              .WithMessage("Password must contain at least 8 characters.")
              .MaximumLength(100);
        }
    }
}
