using ecommerce.frontend.Dtos;
using ecommerce.frontend.Dtos.Orders;
using ecommerce.frontend.Interfaces;
using static System.Net.WebRequestMethods;

namespace ecommerce.frontend.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<OrderDto>> GetOrdersAsync()
        {
            var response = await _httpClient.GetAsync("/api/orders");
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<List<OrderDto>>>();
            if (apiResponse != null && apiResponse.Success)
            {
                return apiResponse.Data ?? new List<OrderDto>();
            }
            return new List<OrderDto>();
        }

        public async Task<OrderDto?> CreateOrderAsync(CreateOrderDto order)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/orders", order);
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<OrderDto>>();
            return apiResponse?.Data;
        }

        public async Task<List<OrderDto>> GetOrdersByCustomerIdAsync(int customerId)
        {
            var response = await _httpClient.GetAsync($"/api/orders/customer/{customerId}");

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<List<OrderDto>>>();

            return apiResponse?.Data ?? new List<OrderDto>();
        }
    }
}
