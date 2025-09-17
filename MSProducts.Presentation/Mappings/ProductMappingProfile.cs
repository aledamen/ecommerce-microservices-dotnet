using AutoMapper;
using MSProducts.Application.Dtos;
using MSProducts.Presentation.Dtos;

namespace MSProducts.Presentation.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<CreateProductRequest, CreateProductDto>();
            CreateMap<UpdateProductRequest, UpdateProductDto>();

            CreateMap<CreateProductDto, CreateProductRequest>();
            CreateMap<UpdateProductDto, UpdateProductRequest>();
        }
    }
}
