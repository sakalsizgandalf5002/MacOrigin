using FluentValidation;
using WebApplication1.DTOs.UserDTO;

namespace WebApplication1.Validators.UserValidators;

public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateValidator()
    {
        When(x => x.Username is not null, () =>
        {
            RuleFor(x => x.Username)
                .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
                .MaximumLength(20).WithMessage("Username must be at most 20 characters long.");
        });

        When(x => x.Email is not null, () =>
        {
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("A valid email address is required.");
        });

        When(x => x.Password is not null, () =>
        {
            RuleFor(x => x.Password)
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.");
        });
    }
}