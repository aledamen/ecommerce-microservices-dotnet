namespace ecommerce.frontend.Dtos
{
    public record OrderItemDto
    {
        public int ProductId { get; init; }
        public string ProductName { get; init; } = string.Empty;
        public decimal UnitPrice { get; init; }
        public int Quantity { get; init; }
        public decimal SubTotal => UnitPrice * Quantity;
    }
}
