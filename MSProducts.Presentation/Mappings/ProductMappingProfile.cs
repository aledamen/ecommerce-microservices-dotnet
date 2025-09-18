using AutoMapper;
using MSProducts.Application.Dtos;
using MSProducts.Domain.Entities;
using MSProducts.Presentation.Dtos;

namespace MSProducts.Presentation.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<CreateProductRequest, CreateProductDto>();
            CreateMap<CreateProductDto, CreateProductRequest>();

            CreateMap<UpdateProductRequest, UpdateProductDto>();
            CreateMap<UpdateProductDto, UpdateProductRequest>();

            CreateMap<ProductResponseDto, Product>();
            CreateMap<Product, ProductResponseDto>();
        }
    }
}
