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
                .MaximumLength(100)
                .WithMessage("Product name cannot exceed 100 characters");

            RuleFor(p => p.Description)
                .MaximumLength(4000)
                .WithMessage("Description cannot exceed 4000 characters");

            RuleFor(p => p.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be greater than or equal to 0");

            RuleFor(p => p.Stock)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stock must be greater than or equal to 0");
        }
    }
}
