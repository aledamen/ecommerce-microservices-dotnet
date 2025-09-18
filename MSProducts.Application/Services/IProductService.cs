using Kernel;
using MSProducts.Application.Dtos;
using MSProducts.Domain.Entities;

namespace MSProducts.Application.Services
{
    public interface IProductService
    {
        Task<Result<IEnumerable<Product>>> GetAllProductsAsync();
        Task<Result<Product>> GetProductByIdAsync(int id);
        Task<Result<Product>> CreateProductAsync(CreateProductDto request);
        Task<Result<Product>> UpdateProductAsync(int id, UpdateProductDto request);
        Task<Result<Product>> DecreaseStockAsync(int id, int quantity);
        Task<Result> DeleteProductAsync(int id);
    }
}
