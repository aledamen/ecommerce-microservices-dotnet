using Microsoft.EntityFrameworkCore;
using MSProducts.Application.Repositories;
using MSProducts.Domain.Entities;
using MSProducts.Infraestructure.Data;

namespace MSProducts.Infraestructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);

            await _dbContext.SaveChangesAsync();

            return product;
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            _dbContext.Products.Update(product);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteProductAsync(Product product)
        {
            _dbContext.Products.Remove(product);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
