using FluentValidation;
using MSOrders.Domain.Entities;

namespace MSOrders.Application.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(o => o.CustomerId)
                .GreaterThan(0)
                .WithMessage("CustomerId must be greater than 0");

            RuleFor(o => o.CustomerFirstName)
                .NotEmpty()
                .WithMessage("CustomerFirstName is required")
                .MaximumLength(50)
                .WithMessage("CustomerFirstName cannot exceed 50 characters");

            RuleFor(o => o.CustomerLastName)
                .NotEmpty()
                .WithMessage("CustomerLastName is required")
                .MaximumLength(50)
                .WithMessage("CustomerLastName cannot exceed 50 characters");

            RuleFor(o => o.OrderDate)
                .LessThanOrEqualTo(_ => DateTime.UtcNow)
                .WithMessage("OrderDate cannot be in the future");

            RuleFor(o => o.Total)
                .GreaterThan(0)
                .WithMessage("Total must be greater than 0");

            RuleFor(o => o.ShippingAddress)
                .NotNull()
                .WithMessage("ShippingAddress is required");

            RuleFor(o => o.OrderItems)
                .NotEmpty()
                .WithMessage("Order must contain at least one OrderItem");

            RuleForEach(o => o.OrderItems).SetValidator(new OrderItemValidator());
        }
    }

    public class OrderItemValidator : AbstractValidator<OrderItem>
    {
        public OrderItemValidator()
        {
            RuleFor(oi => oi.ProductName)
                .NotEmpty()
                .WithMessage("ProductName is required")
                .MaximumLength(100)
                .WithMessage("ProductName cannot exceed 100 characters");

            RuleFor(oi => oi.UnitPrice)
                .GreaterThan(0)
                .WithMessage("UnitPrice must be greater than 0");

            RuleFor(oi => oi.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0");
        }
    }
}
