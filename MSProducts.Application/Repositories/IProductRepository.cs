using MSProducts.Domain;

namespace MSProducts.Application.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task<int> UpdateProductAsync(Product product);
        Task<int> DeleteProductAsync(Product product);
    }
}
