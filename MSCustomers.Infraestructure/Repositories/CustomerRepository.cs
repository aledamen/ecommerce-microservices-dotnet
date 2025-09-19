using Microsoft.EntityFrameworkCore;
using MSCustomers.Application.Repositories;
using MSCustomers.Domain.Entities;
using MSCustomers.Infraestructure.Data;

namespace MSCustomers.Infraestructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _dbContext.Customers.FindAsync(id);
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);

            await _dbContext.SaveChangesAsync();

            return customer;
        }

        public async Task<int> UpdateCustomerAsync(Customer customer)
        {
            _dbContext.Customers.Update(customer);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteCustomerAsync(Customer customer)
        {
            _dbContext.Customers.Remove(customer);

            return await _dbContext.SaveChangesAsync();
        }
    }
}
