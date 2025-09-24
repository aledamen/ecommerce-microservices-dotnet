namespace ecommerce.frontend.Dtos.Customers
{
    public record UpdateCustomerDto(string? FirstName, string? LastName, string? Email, string? PhoneNumber, Address? Address);
}
