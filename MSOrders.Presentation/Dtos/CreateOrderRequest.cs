namespace MSOrders.Presentation.Dtos
{
    public record CreateOrderRequest(List<ProductQuantityRequest> ProductQuantity, int CustomerId);

    public class ProductQuantityRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
