using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PncUniform.Shopping.UniformInventory.Application.Domain.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("CustomerName")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Column("CustomerEmail")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Column("CustomerMobile")]
        public string MobileNumber { get; set; }
    }
}
