using FluentValidation;
using MSCustomers.Domain.Entities;

namespace MSCustomers.Application.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator() 
        { 
            RuleFor(c => c.FirstName)
                .NotEmpty()
                .WithMessage("First name is required")
                .MaximumLength(50)
                .WithMessage("First name cannot exceed 100 characters");

            RuleFor(c => c.LastName)
                .NotEmpty()
                .WithMessage("Last name is required")
                .MaximumLength(50)
                .WithMessage("Last name cannot exceed 100 characters");

            RuleFor(c => c.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("A valid email is required");

            RuleFor(c => c.PhoneNumber)
                .MaximumLength(15)
                .WithMessage("Phone number cannot exceed 15 characters");

            RuleFor(c => c.Address)
                .NotNull()
                .WithMessage("Address is required");
        }

    }
}
