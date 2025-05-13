namespace AutoMapperComplexMappingDemo.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public string OrderDate { get; set; } // Formatted Date
        public decimal Amount { get; set; } // Total amount of the order
        public string CustomerName { get; set; } // Full Name of the customer
        public string CustomerEmail { get; set; }  // Email of the customer
        public string CustomerPhoneNumber { get; set; }  // Contact number
        public AddressDTO Address { get; set; }
        public List<OrderItemDTO> Items { get; set; }

    }
}
