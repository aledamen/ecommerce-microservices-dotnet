using FluentValidation;
using Kernel;
using MSCustomers.Application.Dtos;
using MSCustomers.Application.Repositories;
using MSCustomers.Domain.Entities;
using MSCustomers.Domain.ValueObjects;

namespace MSCustomers.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IValidator<Customer> _validations;
        
        public CustomerService(ICustomerRepository customerRepository, IValidator<Customer> validations)
        {
            _customerRepository = customerRepository;
            _validations = validations;
        }

        public async Task<Result<IEnumerable<Customer>>> GetAllCustoemersAsync()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();

            return Result.Success(customers);
        }

        public async Task<Result<Customer>> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);

            if (customer == null)
                return Result.Fail<Customer>("Customer not found");

            return Result.Success(customer);
        }

        public async Task<Result<Customer>> CreateCustomerAsync(CreateCustomerDto request)
        {
            var customer = new Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = new Address
                {
                    Street = request.Address.Street,
                    City = request.Address.City,
                    Number = request.Address.Number,
                }

            };

            var validationResult = _validations.Validate(customer);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return Result.Fail<Customer>($"Validation failed: {errors}");
            }

            var newCustomer = await _customerRepository.CreateCustomerAsync(customer);

            return Result.Success(newCustomer);
        }

        public async Task<Result<Customer>> UpdateCustomerAsync(int id, UpdateCustomerDto request)
        {
            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(id);

            if (existingCustomer == null)
                return Result.Fail<Customer>($"Customer with id {id} not found");

            existingCustomer.UpdateDetails(
                request.FirstName,
                request.LastName,
                request.Email ?? string.Empty,
                request.PhoneNumber ?? string.Empty,
                new Address
                {
                    Street = request.Address?.Street ?? string.Empty,
                    City = request.Address?.City ?? string.Empty,
                    Number = request.Address?.Number ?? 0,
                }
            );

            var validationResult = _validations.Validate(existingCustomer);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                return Result.Fail<Customer>(errors);
            }

            var result = await _customerRepository.UpdateCustomerAsync(existingCustomer);

            if (result == 0)
                return Result.Fail<Customer>("Failed to update the customer");

            return Result.Success(existingCustomer);
        }

        public async Task<Result> DeleteCustomerAsync(int id)
        {
            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(id);
            if (existingCustomer == null)
                return Result.Fail($"Customer with id {id} not found");

            var result = await _customerRepository.DeleteCustomerAsync(existingCustomer);

            if (result == 0)
                return Result.Fail("Failed to delete the customer");

            return Result.Success();
        }
    }
}
