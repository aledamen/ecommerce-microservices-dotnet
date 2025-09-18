using AutoMapper;
using MSCustomers.Application.Dtos;
using MSCustomers.Domain.Entities;
using MSCustomers.Presentation.Dtos;

namespace MSCustomers.Presentation.Mappings
{
    public class CustomerMappingProfile: Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<CreateCustomerRequest, CreateCustomerDto>();
            CreateMap<CreateCustomerDto, CreateCustomerRequest>();

            CreateMap<UpdateCustomerRequest, UpdateCustomerDto>();
            CreateMap<UpdateCustomerDto, UpdateCustomerRequest>();

            CreateMap<CreateCustomerDto, CustomerResponseDto>();
            CreateMap<CustomerResponseDto, CreateCustomerDto>();

            CreateMap<UpdateCustomerDto, UpdateCustomerDto>();
            CreateMap<UpdateCustomerDto, CustomerResponseDto>();

            CreateMap<Customer, CustomerResponseDto>();
            CreateMap<CustomerResponseDto, Customer>();
        }
    }
}
