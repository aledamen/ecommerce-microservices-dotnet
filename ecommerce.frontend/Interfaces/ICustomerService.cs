using ecommerce.frontend.Dtos.Customers;

namespace ecommerce.frontend.Interfaces
{
    public interface ICustomerService
    {
        public Task<List<CustomerDto>> GetCustomersAsync();
        public Task<CustomerDto?> GetCustomerByIdAsync(int id);
        public Task<CustomerDto?> CreateCustomerAsync(CreateCustomerDto customer);
        public Task<CustomerDto?> UpdateCustomerAsync(int id, UpdateCustomerDto customer);
        public Task<bool> DeleteCustomerAsync(int id);
    }
}
