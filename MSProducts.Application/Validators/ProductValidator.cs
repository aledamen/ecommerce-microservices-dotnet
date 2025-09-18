using FluentValidation;
using MSProducts.Domain.Entities;

namespace MSProducts.Application.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Product name is required")
                .MaximumLength(50)
                .WithMessage("Product name cannot exceed 50 characters");

            RuleFor(p => p.Description)
                .MaximumLength(300)
                .WithMessage("Description cannot exceed 300 characters");

            RuleFor(p => p.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be greater than or equal to 0");

            RuleFor(p => p.Stock)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stock must be greater than or equal to 0");
        }
    }
}
