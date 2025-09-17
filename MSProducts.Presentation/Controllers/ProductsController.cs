using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MSProducts.Application.Dtos;
using MSProducts.Application.Services;
using MSProducts.Presentation.Dtos;

namespace MSProducts.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productService.GetAllProductsAsync();

            if (result.IsSuccess)
                return Ok(result.Value);

            return NotFound(result.Error);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _productService.GetProductByIdAsync(id);

            if (result.IsSuccess)
                return Ok(result.Value);

            return NotFound(result.Error);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest createProductRequest)
        {
            if(createProductRequest == null)
            {
                return BadRequest("Product data is null.");
            }

            var productDto = _mapper.Map<CreateProductDto>(createProductRequest);

            var result = await _productService.CreateProductAsync(productDto);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(GetProductById), new { id = result.Value.Id }, createProductRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductRequest updateProductRequest)
        {
           if (updateProductRequest == null)
            {
                return BadRequest("Product data is null.");
            }
            var updatePproductDto = _mapper.Map<UpdateProductDto>(updateProductRequest);

            var result = await _productService.UpdateProductAsync(id, updatePproductDto);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);

            if (!result.IsSuccess)
                return NotFound(result.Error);

            return NoContent();
        }
    }
}
