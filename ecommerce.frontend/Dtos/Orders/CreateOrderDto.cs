namespace ecommerce.frontend.Dtos.Orders
{
    public record CreateOrderDto(List<ProductQuantityDto> ProductQuantity, int CustomerId);

    public class ProductQuantityDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
