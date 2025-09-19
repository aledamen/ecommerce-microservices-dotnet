using MSOrders.Application.Dtos;

namespace MSOrders.Application.Clients
{
    public interface IProductServiceClient
    {
        public Task<ProductDto?> GetProductByIdAsync(int id);
        public Task<bool> DecreaseStockAsync(int productId, int quantity);
    }
}
