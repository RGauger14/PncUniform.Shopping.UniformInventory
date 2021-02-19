using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PncUniform.Shopping.UniformInventory.Domain.Entities
{
    public class OrderItem
    {
        [Key]
        [Required]
        public int OrderItemId { get; set; }

        [Required]
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }


        [Required]
        [ForeignKey("UniformId")]
        public int UniformId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
