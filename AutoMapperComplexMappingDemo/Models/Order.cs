using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AutoMapperComplexMappingDemo.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }  // Total amount of the order

        [Required]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; } // Navigation Property

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
