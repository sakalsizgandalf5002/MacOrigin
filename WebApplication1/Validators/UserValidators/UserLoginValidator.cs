using FluentValidation;
using WebApplication1.DTOs.UserDTO;

namespace WebApplication1.Validators.UserValidators;

public class UserLoginValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email format is invalid.");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(16).WithMessage("Password must be at least 16 characters.");
    }
}