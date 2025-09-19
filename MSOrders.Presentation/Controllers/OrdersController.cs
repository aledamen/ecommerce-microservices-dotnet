using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MSOrders.Application.Dtos;
using MSOrders.Application.Services;
using MSOrders.Presentation.Contracts;
using MSOrders.Presentation.Dtos;

namespace MSOrders.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(
            IOrderService orderService,
            IMapper mapper,
            ILogger<OrdersController> logger
            )
        {
            _orderService = orderService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderService.GetAllOrdersAsync();

            if (!result.IsSuccess)
            {
                _logger.LogError("Error retrieving orders: {Error}", result.Error);
                return NotFound(ApiResponse<IEnumerable<OrderResponseDto>>.ErrorResponse(result.Error, 400));
            }

            var orders = _mapper.Map<IEnumerable<OrderResponseDto>>(result.Value);

            _logger.LogInformation("Retrieved {Count} orders", orders.Count());
            return Ok(ApiResponse<IEnumerable<OrderResponseDto>>.SuccessResponse(orders, 200));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _orderService.GetOrderByIdAsync(id);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("Order with ID {Id} not found: {Error}", id, result.Error);
                return NotFound(ApiResponse<OrderResponseDto>.ErrorResponse(result.Error, 404));
            }

            var order = _mapper.Map<OrderResponseDto>(result.Value);

            _logger.LogInformation("Retrieved order with ID {Id}", id);
            return Ok(ApiResponse<OrderResponseDto>.SuccessResponse(order, 200));
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetOrdersByCustomerId(int customerId)
        {
            var result = await _orderService.GetOrdersByCustomerIdAsync(customerId);

            if (!result.IsSuccess)
            {
                _logger.LogError("Error retrieving orders for customer ID {CustomerId}: {Error}", customerId, result.Error);
                return NotFound(ApiResponse<IEnumerable<OrderResponseDto>>.ErrorResponse(result.Error, 400));
            }

            var orders = _mapper.Map<IEnumerable<OrderResponseDto>>(result.Value);

            _logger.LogInformation("Retrieved {Count} orders for customer ID {CustomerId}", orders.Count(), customerId);
            return Ok(ApiResponse<IEnumerable<OrderResponseDto>>.SuccessResponse(orders, 200));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest createOrderRequest)
        {
            if (createOrderRequest == null)
            {
                _logger.LogWarning("CreateOrderRequest is null");
                return BadRequest(ApiResponse<OrderResponseDto>.ErrorResponse("Order data is null", 400));
            }

            var orderDto = _mapper.Map<CreateOrderDto>(createOrderRequest);

            var result = await _orderService.CreateOrderAsync(orderDto);

            if (!result.IsSuccess)
            {
                _logger.LogError("Error creating order: {Error}", result.Error);
                return BadRequest(ApiResponse<OrderResponseDto>.ErrorResponse(result.Error, 400));
            }

            var createdOrder = _mapper.Map<OrderResponseDto>(result.Value);

            _logger.LogInformation("Created order with ID {Id}", result.Value.Id);
            return CreatedAtAction(
                nameof(GetOrderById),
                new { id = result.Value.Id },
                ApiResponse<OrderResponseDto>.SuccessResponse(createdOrder, 201)
            );
        }
    }
}
