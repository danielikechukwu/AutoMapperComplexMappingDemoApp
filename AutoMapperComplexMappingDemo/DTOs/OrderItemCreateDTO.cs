using System.ComponentModel.DataAnnotations;

namespace AutoMapperComplexMappingDemo.DTOs
{
    public class OrderItemCreateDTO
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
