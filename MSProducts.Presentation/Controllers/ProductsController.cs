using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MSProducts.Application.Dtos;
using MSProducts.Application.Services;
using MSProducts.Domain.Entities;
using MSProducts.Presentation.Contracts;
using MSProducts.Presentation.Dtos;

namespace MSProducts.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(
            IProductService productService, 
            IMapper mapper, 
            ILogger<ProductsController> logger
            )
        {
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productService.GetAllProductsAsync();

            if (!result.IsSuccess)
            {
                _logger.LogError("Error retrieving products: {Error}", result.Error);
                return NotFound(ApiResponse<IEnumerable<ProductResponseDto>>.ErrorResponse(result.Error, 400));
            }

            var products = _mapper.Map<IEnumerable<ProductResponseDto>>(result.Value);
            
            _logger.LogInformation("Retrieved {Count} products", products.Count());
            return Ok(ApiResponse<IEnumerable<ProductResponseDto>>.SuccessResponse(products, 200));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _productService.GetProductByIdAsync(id);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("Product with ID {Id} not found: {Error}", id, result.Error);
                return NotFound(ApiResponse<ProductResponseDto>.ErrorResponse(result.Error, 404));
            }

            var product = _mapper.Map<ProductResponseDto>(result.Value);

            _logger.LogInformation("Retrieved product with ID {Id}", id);
            return Ok(ApiResponse<ProductResponseDto>.SuccessResponse(product, 200));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest createProductRequest)
        {
            if(createProductRequest == null)
            {
                _logger.LogWarning("CreateProductRequest is null");
                return BadRequest(ApiResponse<Product>.ErrorResponse("Product data is null", 400));
            }

            var productDto = _mapper.Map<CreateProductDto>(createProductRequest);

            var result = await _productService.CreateProductAsync(productDto);

            if (!result.IsSuccess)
            {
                _logger.LogError("Error creating product: {Error}", result.Error);
                return BadRequest(ApiResponse<Product>.ErrorResponse(result.Error, 400));
            }
                
            var createdProduct = _mapper.Map<ProductResponseDto>(result.Value);

            _logger.LogInformation("Created product with ID {Id}", result.Value.Id);
            return CreatedAtAction(
                nameof(GetProductById), 
                new { id = result.Value.Id }, 
                ApiResponse<Product>.SuccessResponse(result.Value, 201)
            );
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductRequest updateProductRequest)
        {
           if (updateProductRequest == null)
            {
                _logger.LogWarning("UpdateProductRequest is null");
                return BadRequest(ApiResponse<ProductResponseDto>.ErrorResponse("Product data is null", 400));
            }

            var updatePproductDto = _mapper.Map<UpdateProductDto>(updateProductRequest);

            var result = await _productService.UpdateProductAsync(id, updatePproductDto);

            if (!result.IsSuccess)
            {
                _logger.LogError("Error updating product with ID {Id}: {Error}", id, result.Error);
                return BadRequest(ApiResponse<ProductResponseDto>.ErrorResponse(result.Error, 400));
            }
                
            var updatedProduct = _mapper.Map<ProductResponseDto>(result.Value);
            
            _logger.LogInformation("Updated product with ID {Id}", id);
            return Ok(ApiResponse<ProductResponseDto>.SuccessResponse(updatedProduct));
        }

        [HttpPost("{id}/decrease-stock")]
        public async Task<IActionResult> DecreaseStock(int id, [FromBody] DecreaseStockRequest request)
        {
            if (request.Quantity < 1)
            {
                _logger.LogWarning("Invalid quantity {Quantity} for decreasing stock of product ID {Id}", request.Quantity, id);
                return BadRequest(ApiResponse<ProductResponseDto>.ErrorResponse("Quantity must be at least 1", 400));
            }
                
            var result = await _productService.DecreaseStockAsync(id, request.Quantity);

            if (!result.IsSuccess)
            {
                _logger.LogError("Error decreasing stock for product ID {Id}: {Error}", id, result.Error);
                return BadRequest(ApiResponse<ProductResponseDto>.ErrorResponse(result.Error, 400));
            }

            var updatedProduct = _mapper.Map<ProductResponseDto>(result.Value);

            _logger.LogInformation("Decreased stock of product ID {Id} by {Quantity}", id, request.Quantity);
            return Ok(ApiResponse<ProductResponseDto>.SuccessResponse(updatedProduct));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);

            if (!result.IsSuccess)
            {
                _logger.LogError("Error deleting product with ID {Id}: {Error}", id, result.Error);
                return NotFound(ApiResponse<ProductResponseDto>.ErrorResponse("Product not found", 404));
            }

            _logger.LogInformation("Deleted product with ID {Id}", id);
            return NoContent();
        }
    }
}
