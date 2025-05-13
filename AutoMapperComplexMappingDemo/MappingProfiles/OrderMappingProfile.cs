using AutoMapper;
using AutoMapperComplexMappingDemo.DTOs;
using AutoMapperComplexMappingDemo.Models;

namespace AutoMapperComplexMappingDemo.MappingProfiles
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile() {

            // CreateMap: Defines a mapping between two types.
            // ForMember: Customizes the mapping for specific properties.
            // Mapping configuration for fetching order details
            // Maps the Order entity to OrderDTO for data transfer
            CreateMap<Order, OrderDTO>()
                // Map the Order.Id property to OrderDTO.OrderId
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                // Map OrderDate from DateTime to a formatted string (yyyy-MM-dd)
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate.ToString("yyyy-MM-dd")))
                // Concatenate Customer.FirstName and Customer.LastName to form CustomerName in OrderDTO
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => $"{src.Customer.FirstName} {src.Customer.LastName}"))
                // Map the Customer.Email property to OrderDTO.CustomerEmail
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.Customer.Email))
                // Map the Customer.PhoneNumber property to OrderDTO.CustomerPhoneNumber
                .ForMember(dest => dest.CustomerPhoneNumber, opt => opt.MapFrom(src => src.Customer.PhoneNumber))
                // Map the Customer.Address (complex type) to OrderDTO.Address (DTO for complex types)
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Customer.Address))
                // Map the collection of OrderItems to a list of OrderItemDTO in OrderDTO.Items
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems));

            // Mapping configuration for order items
            // Maps the OrderItem entity to OrderItemDTO for data transfer
            CreateMap<OrderItem, OrderItemDTO>()
                // Map the Product.Name property to OrderItemDTO.ProductName
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                // Map the Product.Price property to OrderItemDTO.ProductPrice
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price))
                // Calculate and map the total price (Product.Price * Quantity) to OrderItemDTO.TotalPrice
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Product.Price * src.Quantity));

            // Mapping configuration for customer addresses
            // Maps the Address entity to AddressDTO for data transfer
            CreateMap<Address, AddressDTO>(); //Propery Mapping is not required as both contains the same property names

            // Mapping configuration for creating orders
            // Maps the OrderCreateDTO (data received from client) to the Order entity
            CreateMap<OrderCreateDTO, Order>()
                // Set the OrderDate to the current date and time (DateTime.Now) when creating an order
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => DateTime.Now))
                // Ignore the Amount property during the mapping, as it will be calculated later
                .ForMember(dest => dest.Amount, opt => opt.Ignore())
                // Map the list of OrderItemCreateDTO to the OrderItems property in the Order entity
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.Items));

            //Property mapping is not required since they contain same property.
            CreateMap<OrderItemCreateDTO, OrderItem>();

        }

    }
}
