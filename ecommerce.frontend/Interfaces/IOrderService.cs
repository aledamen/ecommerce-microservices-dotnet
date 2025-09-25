using ecommerce.frontend.Dtos.Orders;

namespace ecommerce.frontend.Interfaces
{
    public interface IOrderService
    {
        public Task<List<OrderDto>> GetOrdersAsync();
        public Task<OrderDto?> CreateOrderAsync(CreateOrderDto order);
        public Task<List<OrderDto>> GetOrdersByCustomerIdAsync(int customerId);
    }
}
