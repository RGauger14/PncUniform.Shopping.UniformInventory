using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PncUniform.Shopping.UniformInventory.Application.Domain.Entities
{
    public class OrderItem
    {
        [Key]
        [Required]
        public int OrderItemId { get; set; }

        [Required]
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }

        public Order Order { get; set; }


        [Required]
        [ForeignKey("UniformId")]
        public int UniformId { get; set; }

        public Uniform Uniform { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
