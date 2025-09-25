using System.Net.Http.Json;
using ecommerce.frontend.Dtos;
using ecommerce.frontend.Dtos.Customers;
using ecommerce.frontend.Interfaces;

namespace ecommerce.frontend.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CustomerDto>> GetCustomersAsync()
        {
            var response = await _httpClient.GetAsync("/api/customers");
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<List<CustomerDto>>>();
            if (apiResponse != null && apiResponse.Success)
            {
                return apiResponse.Data ?? new List<CustomerDto>();
            }
            return new List<CustomerDto>();
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/customers/{id}");
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<CustomerDto>>();
            return apiResponse?.Data;
        }

        public async Task<CustomerDto?> CreateCustomerAsync(CreateCustomerDto customer)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/customers", customer);
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<CustomerDto>>();
            return apiResponse?.Data;
        }

        public async Task<CustomerDto?> UpdateCustomerAsync(int id, UpdateCustomerDto customer)
        {
            var response = await _httpClient.PatchAsJsonAsync($"/api/customers/{id}", customer);
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<CustomerDto>>();
            return apiResponse?.Data;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/customers/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
