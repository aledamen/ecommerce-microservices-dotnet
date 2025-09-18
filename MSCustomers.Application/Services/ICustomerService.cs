using Kernel;
using MSCustomers.Application.Dtos;
using MSCustomers.Domain.Entities;

namespace MSCustomers.Application.Services
{
    public interface ICustomerService
    {
        Task<Result<IEnumerable<Customer>>> GetAllCustomersAsync();
        Task<Result<Customer>> GetCustomerByIdAsync(int id);
        Task<Result<Customer>> CreateCustomerAsync(CreateCustomerDto request);
        Task<Result<Customer>> UpdateCustomerAsync(int id, UpdateCustomerDto request);
        Task<Result> DeleteCustomerAsync(int id);
    }
}
