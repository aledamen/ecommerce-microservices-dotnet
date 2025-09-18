using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MSCustomers.Application.Dtos;
using MSCustomers.Application.Services;
using MSCustomers.Presentation.Contracts;
using MSCustomers.Presentation.Dtos;

namespace MSCustomers.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(
            ICustomerService customerService,
            IMapper mapper,
            ILogger<CustomersController> logger
            )
        {
            _customerService = customerService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _customerService.GetAllCustomersAsync();

            if (!result.IsSuccess)
            {
                _logger.LogError("Error retrieving customers: {Error}", result.Error);
                return NotFound(ApiResponse<IEnumerable<CustomerResponseDto>>.ErrorResponse(result.Error, 400));
            }

            var customers = _mapper.Map<IEnumerable<CustomerResponseDto>>(result.Value);

            _logger.LogInformation("Retrieved {Count} customers", customers.Count());
            return Ok(ApiResponse<IEnumerable<CustomerResponseDto>>.SuccessResponse(customers, 200));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var result = await _customerService.GetCustomerByIdAsync(id);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("Customer with ID {Id} not found: {Error}", id, result.Error);
                return NotFound(ApiResponse<CustomerResponseDto>.ErrorResponse(result.Error, 404));
            }

            var customer = _mapper.Map<CustomerResponseDto>(result.Value);

            _logger.LogInformation("Retrieved customer with ID {Id}", id);
            return Ok(ApiResponse<CustomerResponseDto>.SuccessResponse(customer, 200));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest createCustomerRequest)
        {
            if (createCustomerRequest == null)
            {
                _logger.LogWarning("CreateCustomerRequest is null");
                return BadRequest(ApiResponse<CustomerResponseDto>.ErrorResponse("Customer data is null", 400));
            }

            var customerDto = _mapper.Map<CreateCustomerDto>(createCustomerRequest);

            var result = await _customerService.CreateCustomerAsync(customerDto);

            if (!result.IsSuccess)
            {
                _logger.LogError("Error creating customer: {Error}", result.Error);
                return BadRequest(ApiResponse<CustomerResponseDto>.ErrorResponse(result.Error, 400));
            }

            var createdCustomer = _mapper.Map<CustomerResponseDto>(result.Value);

            _logger.LogInformation("Created customer with ID {Id}", result.Value.Id);
            return CreatedAtAction(
                nameof(GetCustomerById),
                new { id = result.Value.Id },
                ApiResponse<CustomerResponseDto>.SuccessResponse(createdCustomer, 201)
            );
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerRequest updateCustomerRequest)
        {
            if (updateCustomerRequest == null)
            {
                _logger.LogWarning("UpdateCustomerRequest is null");
                return BadRequest(ApiResponse<CustomerResponseDto>.ErrorResponse("Customer data is null", 400));
            }

            var updateCustomertDto = _mapper.Map<UpdateCustomerDto>(updateCustomerRequest);

            var result = await _customerService.UpdateCustomerAsync(id, updateCustomertDto);

            if (!result.IsSuccess)
            {
                _logger.LogError("Error updating customer with ID {Id}: {Error}", id, result.Error);
                return BadRequest(ApiResponse<CustomerResponseDto>.ErrorResponse(result.Error, 400));
            }

            var updatedCustomer = _mapper.Map<CustomerResponseDto>(result.Value);

            _logger.LogInformation("Updated customer with ID {Id}", id);
            return Ok(ApiResponse<CustomerResponseDto>.SuccessResponse(updatedCustomer));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await _customerService.DeleteCustomerAsync(id);

            if (!result.IsSuccess)
            {
                _logger.LogError("Error deleting customer with ID {Id}: {Error}", id, result.Error);
                return NotFound(ApiResponse<CustomerResponseDto>.ErrorResponse("Customer not found", 404));
            }

            _logger.LogInformation("Deleted customer with ID {Id}", id);
            return NoContent();
        }
    }
}
