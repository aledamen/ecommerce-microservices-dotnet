using MSOrders.Application.Clients;
using MSOrders.Application.Contracts;
using MSOrders.Application.Dtos;
using System.Net.Http.Json;

namespace MSOrders.Infraestructure.Clients
{
    public class CustomerServiceClient : ICustomerServiceClient
    {
        private readonly HttpClient _httpClient;

        public CustomerServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/customers/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<CustomerDto>>();
            return apiResponse?.Data;
        }
    }
}
