using FluentValidation;
using FluentValidation.Validators;
using WebApplication1.DTOs.CategoryDTO;
namespace WebApplication1.Validators.CategoryValidators;

public class CategoryCreateValidator : AbstractValidator<CategoryCreateDto>
{
  public CategoryCreateValidator()
  {
    RuleFor(x=>x.Name)
      .NotEmpty().WithMessage("CategoryName is required.")
      .MaximumLength(30).WithMessage("CategoryName must not exceed 30 characters.");
  }
}