using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AutoMapperComplexMappingDemo.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; } // Navigation Property

        [Required]
        public int Quantity { get; set; } // Quantity of the product in the order

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ProductPrice { get; set; }

        [Required]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; } // Navigation Property
    }
}
