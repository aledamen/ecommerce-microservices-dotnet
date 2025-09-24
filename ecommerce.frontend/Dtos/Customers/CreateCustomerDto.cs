namespace ecommerce.frontend.Dtos.Customers
{
    public record CreateCustomerDto(string FirstName, string LastName, string Email, string? PhoneNumber, Address Address);
}
