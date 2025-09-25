namespace ecommerce.frontend.Dtos.Orders
{
    public class CreateOrderDto
    {
        public List<ProductQuantityDto> ProductQuantity { get; set; } = new();
        public int CustomerId { get; set; }
    }

    public class ProductQuantityDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
