using MSProducts.Application.Dtos;
using MSProducts.Domain;

namespace MSProducts.Application.Services
{
    public interface IProductService
    {
        Task<Result<IEnumerable<Product>>> GetAllProductsAsync();
        Task<Result<Product>> GetProductByIdAsync(int id);
        Task<Result<Product>> CreateProductAsync(CreateProductDto request);
        Task<Result<Product>> UpdateProductAsync(int id, UpdateProductDto request);
        Task<Result> DeleteProductAsync(int id);
    }
}
