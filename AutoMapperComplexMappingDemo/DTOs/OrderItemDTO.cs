namespace AutoMapperComplexMappingDemo.DTOs
{
    public class OrderItemDTO
    {
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; } // ProductPrice * Quantity
    }
}
