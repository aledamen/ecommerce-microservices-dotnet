using MSCustomers.Domain.ValueObjects;

namespace MSCustomers.Presentation.Dtos
{
    public record UpdateCustomerRequest(string? FirstName, string? LastName, string? Email, string? PhoneNumber, Address? Address);
}
