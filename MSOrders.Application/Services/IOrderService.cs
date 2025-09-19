using Kernel;
using MSOrders.Application.Dtos;
using MSOrders.Domain.Entities;

namespace MSOrders.Application.Services
{
    public interface IOrderService
    {
        Task<Result<IEnumerable<Order>>> GetAllOrdersAsync();
        Task<Result<Order>> GetOrderByIdAsync(int id);
        Task<Result<IEnumerable<Order>>> GetOrdersByCustomerIdAsync(int customerId);
        Task<Result<Order>> CreateOrderAsync(CreateOrderDto order);
    }
}
