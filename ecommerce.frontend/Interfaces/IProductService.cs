using ecommerce.frontend.Dtos.Products;

namespace ecommerce.frontend.Interfaces
{
    public interface IProductService
    {
        public Task<List<ProductDto>> GetProductsAsync();
        public Task<ProductDto?> GetProductByIdAsync(int id);
        public Task<ProductDto?> CreateProductAsync(CreateProductDto product);
        public Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto product);
        public Task<bool> DeleteProductAsync(int id);
    }
}
