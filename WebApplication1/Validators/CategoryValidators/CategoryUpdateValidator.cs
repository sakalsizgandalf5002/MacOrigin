using FluentValidation;
using WebApplication1.DTOs.CategoryDTO;
namespace WebApplication1.Validators.CategoryValidators;

public class CategoryUpdateValidator : AbstractValidator<CategoryUpdateDto>
{
    public CategoryUpdateValidator()
    {
        RuleFor(x=>x.Name)
            .NotEmpty().WithMessage("CategoryName is required.")
            .MaximumLength(30).WithMessage("CategoryName must not exceed 30 characters.");
    }
}