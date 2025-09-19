using MSOrders.Domain.ValueObjects;

namespace MSOrders.Application.Dtos
{
    public record CustomerDto(int Id, string FirstName, string LastName, string Email, ShippingAddress Address);
}
