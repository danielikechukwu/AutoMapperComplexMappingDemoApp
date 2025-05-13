using System.ComponentModel.DataAnnotations;

namespace AutoMapperComplexMappingDemo.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; } // Email of the customer

        [Required]
        public string PhoneNumber { get; set; } // Contact number of the customer

        public Address Address { get; set; } // Navigation property for the Address

        public List<Order> Orders { get; set; } // Collection Navigation property for the Orders
    }
}
