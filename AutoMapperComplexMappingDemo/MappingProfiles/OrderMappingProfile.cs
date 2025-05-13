using AutoMapper;
using AutoMapperComplexMappingDemo.DTOs;
using AutoMapperComplexMappingDemo.Models;

namespace AutoMapperComplexMappingDemo.MappingProfiles
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile() {

            CreateMap<Order, OrderDTO>()
                .ForMember(
                dest => dest.OrderId, 
                opt => opt.MapFrom<int>(src => src.Id))
                .ForMember(
                dest => dest.OrderDate, 
                opt => opt.MapFrom<string>(src => src.OrderDate.ToString("yyyy-MM-dd")))
                .ForMember(
                dest => dest.CustomerName, 
                opt => opt.MapFrom<string>(src => $"{src.Customer.FirstName} {src.Customer.LastName}"))
                .ForMember(
                dest => dest.CustomerEmail, 
                opt => opt.MapFrom<string>(src => src.Customer.Email))
                .ForMember(
                dest => dest.CustomerPhoneNumber, 
                opt => opt.MapFrom<string>(src => src.Customer.PhoneNumber))
                .ForMember(
                dest => dest.Address, 
                opt => opt.MapFrom<Address>(src => src.Customer.Address))
                .ForMember(
                dest => dest.Items, 
                opt => opt.MapFrom<ICollection<OrderItem>>(src => src.OrderItems));

            // Mapping configuration for customer addresses
            // Maps the Address entity to AddressDTO for data transfer
            CreateMap<Address, AddressDTO>(); //Propery Mapping is not required as both contains the same property names

            // Mapping configuration for order items
            // Maps the OrderItem entity to OrderItemDTO for data transfer
            CreateMap<OrderItem, OrderItemDTO>()
                // Map the Product.Name property to OrderItemDTO.ProductName
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                // Map the Product.Price property to OrderItemDTO.ProductPrice
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price))
                // Calculate and map the total price (Product.Price * Quantity) to OrderItemDTO.TotalPrice
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Product.Price * src.Quantity));

            //Property mapping is not required since they contain same property.
            CreateMap<OrderItemCreateDTO, OrderItem>();

        }

    }
}
