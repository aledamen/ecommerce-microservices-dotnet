using AutoMapper;
using MSOrders.Application.Dtos;
using MSOrders.Domain.Entities;
using MSOrders.Presentation.Dtos;

namespace MSOrders.Presentation.Mappings
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<CreateOrderRequest, CreateOrderDto>();

            CreateMap<ProductQuantityRequest, ProductQuantityDto>();

            CreateMap<OrderResponseDto, Order>();

            CreateMap<OrderItem, OrderItemResponseDto>();

            CreateMap<Order, OrderResponseDto>()
                .ForMember(dest => dest.Products,
                           opt => opt.MapFrom(src => src.OrderItems));
        }
    }
}
