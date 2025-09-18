using MSCustomers.Domain.ValueObjects;

namespace MSCustomers.Application.Dtos
{
    public record CreateCustomerDto(string FirstName, string LastName, string Email, string? PhoneNumber, Address Address);
}
