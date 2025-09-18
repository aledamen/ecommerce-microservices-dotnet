using FluentValidation;
using Kernel;
using MSProducts.Application.Dtos;
using MSProducts.Application.Repositories;
using MSProducts.Domain.Entities;

namespace MSProducts.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidator<Product> _validations;

        public ProductService(IProductRepository productRepository, IValidator<Product> validations)
        {
            _productRepository = productRepository;
            _validations = validations;
        }

        public async Task<Result<IEnumerable<Product>>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();

            return Result.Success(products);
        }

        public async Task<Result<Product>> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
                return Result.Fail<Product>("Product not found");

            return Result.Success(product);
        }

        public async Task<Result<Product>> CreateProductAsync(CreateProductDto request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock
            };

            var validationResult = _validations.Validate(product);
            
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return Result.Fail<Product>($"Validation failed: {errors}");
            }

            var newProduct = await _productRepository.CreateProductAsync(product);

            return Result.Success(newProduct);
        }

        public async Task<Result<Product>> UpdateProductAsync(int id, UpdateProductDto request)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(id);

            if (existingProduct == null)
                return Result.Fail<Product>($"Product with id {id} not found");

            existingProduct.UpdateDetails(
                request.Name,
                request.Description,
                request.Price,
                request.Stock
            );

            var validationResult = _validations.Validate(existingProduct);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                return Result.Fail<Product>(errors);
            }

            var result = await _productRepository.UpdateProductAsync(existingProduct);

            if (result == 0)
                return Result.Fail<Product>("Failed to update the product");

            return Result.Success(existingProduct);
        }

        public async Task<Result<Product>> DecreaseStockAsync(int id, int quantity)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
                return Result.Fail<Product>($"Product with id {id} not found");

            if (quantity > product.Stock)
                return Result.Fail<Product>("Quantity of selling cannot be greater than current stock");

            product.DecreaseStock(quantity);

            var result = await _productRepository.UpdateProductAsync(product);

            if(result == 0)
                return Result.Fail<Product>("Failed to update the product");

            return Result.Success(product);
        }

        public async Task<Result> DeleteProductAsync(int id)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(id);

            if (existingProduct == null)
                return Result.Fail($"Product with id {id} not found");

            var result = await _productRepository.DeleteProductAsync(existingProduct);

            if (result == 0)
                return Result.Fail("Failed to delete the product");

            return Result.Success();
        }
    }
}
