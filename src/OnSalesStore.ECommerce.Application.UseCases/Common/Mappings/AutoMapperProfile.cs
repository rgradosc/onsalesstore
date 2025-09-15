using AutoMapper;
using OnSalesStore.ECommerce.Application.DTO;
using OnSalesStore.ECommerce.Application.UseCases.Customers.Commands.CreateCustomerCommand;
using OnSalesStore.ECommerce.Application.UseCases.Customers.Commands.UpdateCustomerCommand;
using OnSalesStore.ECommerce.Domain.Entities;
using OnSalesStore.ECommerce.Domain.Events;

namespace OnSalesStore.ECommerce.Application.UseCases.Common.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Discount, DiscountDTO>().ReverseMap();
            CreateMap<Discount, DiscountCreatedEvent>().ReverseMap();

            CreateMap<Customer, CreateCustomerCommand>().ReverseMap();
            CreateMap<Customer, UpdateCustomerCommand>().ReverseMap();

        }
    }
}
