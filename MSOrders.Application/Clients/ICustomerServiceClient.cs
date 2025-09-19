using MSOrders.Application.Dtos;

namespace MSOrders.Application.Clients
{
    public interface ICustomerServiceClient
    {
        public Task<CustomerDto?> GetCustomerByIdAsync(int id);
    }
}
