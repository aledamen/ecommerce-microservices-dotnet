using MSOrders.Application.Clients;
using MSOrders.Application.Contracts;
using MSOrders.Application.Dtos;
using System.Net.Http.Json;

namespace MSOrders.Infraestructure.Clients
{
    public class ProductServiceClient : IProductServiceClient
    {
        private readonly HttpClient _httpClient;

        public ProductServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/products/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<ProductDto>>();

            return apiResponse?.Data;
        }

        public async Task<bool> DecreaseStockAsync(int productId, int quantity)
        {
            var response = await _httpClient.PostAsJsonAsync(
                $"/api/products/{productId}/decrease-stock", new { quantity });
            
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<ProductDto>>();

            return apiResponse?.Data != null;
        }
    }
}
