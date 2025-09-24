using ecommerce.frontend.Dtos;
using ecommerce.frontend.Dtos.Products;
using ecommerce.frontend.Interfaces;

namespace ecommerce.frontend.Services
{
    public class ProductService: IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient http)
        {
            _httpClient = http;
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync("/api/products");
            response.EnsureSuccessStatusCode();

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<List<ProductDto>>>();
            if (apiResponse != null && apiResponse.Success)
            {
                return apiResponse.Data ?? new List<ProductDto>();
            }

            return new List<ProductDto>();
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/products/{id}");
            response.EnsureSuccessStatusCode();

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<ProductDto>>();
            return apiResponse?.Data;
        }

        public async Task<ProductDto?> CreateProductAsync(CreateProductDto product)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/products", product);
            response.EnsureSuccessStatusCode();

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<ProductDto>>();
            return apiResponse?.Data;
        }

        public async Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto product)
        {
            var response = await _httpClient.PatchAsJsonAsync($"/api/products/{id}", product);
            response.EnsureSuccessStatusCode();

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<ProductDto>>();
            return apiResponse?.Data;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/products/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
