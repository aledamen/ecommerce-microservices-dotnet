namespace ecommerce.frontend.Dtos.Customers
{
    public record CustomerDto(int Id, string FirstName, string LastName, string Email, string? PhoneNumber, Address Address);
}
