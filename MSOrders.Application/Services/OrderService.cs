using FluentValidation;
using Kernel;
using MSOrders.Application.Clients;
using MSOrders.Application.Dtos;
using MSOrders.Application.Repositories;
using MSOrders.Domain.Entities;
using MSOrders.Domain.ValueObjects;

namespace MSOrders.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IValidator<Order> _validations;
        private readonly ICustomerServiceClient _customerClient;
        private readonly IProductServiceClient _productClient;

        public OrderService(
            IOrderRepository orderRepository, 
            IValidator<Order> validations,
            ICustomerServiceClient customerClient,
            IProductServiceClient productClient
            )
        {
            _orderRepository = orderRepository;
            _validations = validations;
            _customerClient = customerClient;
            _productClient = productClient;
        }

        public async Task<Result<IEnumerable<Order>>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            
            return Result.Success(orders);
        }

        public async Task<Result<Order>> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);

            if (order == null)
                return Result.Fail<Order>("Order not found");

            return Result.Success(order);
        }

        public async Task<Result<IEnumerable<Order>>> GetOrdersByCustomerIdAsync(int customerId)
        {
            var orders = await _orderRepository.GetOrdersByCustomerIdAsync(customerId);
            
            return Result.Success(orders);
        }

        public async Task<Result<Order>> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            var customer = await _customerClient.GetCustomerByIdAsync(createOrderDto.CustomerId);

            if (customer == null)
                return Result.Fail<Order>("Customer not found");

            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                CustomerId = customer.Id,
                CustomerFirstName = customer.FirstName,
                CustomerLastName = customer.LastName,
                ShippingAddress = new ShippingAddress
                {
                    Street = customer.Address.Street,
                    City = customer.Address.City,
                    Number = customer.Address.Number,
                },
                OrderItems = new List<OrderItem>()
            };

            foreach (var product in createOrderDto.ProductQuantity)
            {
                var theProduct = await _productClient.GetProductByIdAsync(product.ProductId);
                if (theProduct == null)
                    return Result.Fail<Order>($"Product {product.ProductId} not found");

                if (theProduct.Stock < product.Quantity)
                    return Result.Fail<Order>($"Insufficient stock for {theProduct.Name}");

                order.AddOrderItem(new OrderItem
                {
                    ProductId = theProduct.Id,
                    ProductName = theProduct.Name,
                    UnitPrice = theProduct.Price,
                    Quantity = product.Quantity
                });

            }

            var validationResult = _validations.Validate(order);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return Result.Fail<Order>($"Validation failed: {errors}");
            }

            var orderCreated = await _orderRepository.CreateOrderAsync(order);

            var decreaseStockTasks = createOrderDto.ProductQuantity.Select(pq =>
                _productClient.DecreaseStockAsync(pq.ProductId, pq.Quantity)).ToList();

            await Task.WhenAll(decreaseStockTasks);

            return Result.Success(orderCreated);
        }
    }
}
