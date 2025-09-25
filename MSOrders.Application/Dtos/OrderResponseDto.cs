using MSOrders.Domain.Entities;
using MSOrders.Domain.ValueObjects;

namespace MSOrders.Application.Dtos
{
    public record OrderResponseDto
    {
        public int Id { get; init; }
        public DateTime OrderDate { get; init; }
        public decimal Total { get; init; }
        public required List<OrderItemResponseDto> Orders { get; init; }
        public required ShippingAddress ShippingAddress { get; init; }
        public required string CustomerFirstName { get; init; }
        public required string CustomerLastName { get; init; }
    }

    public record OrderItemResponseDto
    {
        public int ProductId { get; init; }
        public string ProductName { get; init; } = string.Empty;
        public decimal UnitPrice { get; init; }
        public int Quantity { get; init; }
        public decimal SubTotal => UnitPrice * Quantity;
    }
}
