using FluentValidation;

namespace ToDoAPI.Features.Auth.Login
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Invalid email format.")
                .MaximumLength(255);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .MaximumLength(100);
        }
    }
}
