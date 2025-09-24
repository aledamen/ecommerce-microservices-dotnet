namespace ecommerce.frontend.Dtos.Orders
{
    public record OrderDto
    {
        public int Id { get; init; }
        public DateTime OrderDate { get; init; }
        public decimal Total { get; init; }
        public required List<OrderItemDto> Products { get; init; }
        public required Address ShippingAddress { get; init; }
        public required string CustomerFirstName { get; init; }
        public required string CustomerLastName { get; init; }
    }
}
